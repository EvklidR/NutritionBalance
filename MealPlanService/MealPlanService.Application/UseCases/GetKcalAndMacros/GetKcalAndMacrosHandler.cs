using MediatR;
using MealPlanService.Domain.Interfaces;
using MealPlanService.Domain.Enums;
using MealPlanService.Application.Exceptions;
using MealPlanService.Application.Models;

namespace MealPlanService.Application.UseCases
{
    public class GetKcalAndMacrosHandler : IRequestHandler<GetKcalAndMacrosQuery, DailyNeedsResponse>
    {
        private readonly IMealPlanRepository _mealPlanRepository;

        public GetKcalAndMacrosHandler(IMealPlanRepository mealPlanRepository)
        {
            _mealPlanRepository = mealPlanRepository;
        }

        public async Task<DailyNeedsResponse> Handle(GetKcalAndMacrosQuery request, CancellationToken cancellationToken)
        {
            var mealPlan = await _mealPlanRepository.GetByIdAsync(request.mealPlanId);
            if (mealPlan == null)
                throw new NotFoundException("Meal plan not found");

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            int daysPassed = currentDate.DayNumber - request.startDate.DayNumber;

            var mealPlanDay = mealPlan.Days.FirstOrDefault(d => d.NumberOfDay == daysPassed%mealPlan.Days.Count + 1);
            if (mealPlanDay == null)
                throw new NotFoundException("Meal plan day not found");

            DailyNeedsResponse response = new DailyNeedsResponse();

            response.Calories = mealPlanDay.CaloriePercentage * request.daylyKcal;


            foreach(var nutrient in mealPlanDay.NutrientsOfDay) 
            {
                switch (nutrient.CalculationType)
                {
                    case CalculationType.PerKg:
                        if (nutrient.NutrientType == NutrientType.Protein)
                            response.Proteins = (double)nutrient.Value! * request.bodyWeight;
                        else if (nutrient.NutrientType == NutrientType.Fat)
                            response.Fats = (double)nutrient.Value! * request.bodyWeight;
                        else if (nutrient.NutrientType == NutrientType.Carbohydrate)
                            response.Carbohydrates = (double)nutrient.Value! * request.bodyWeight;
                        break;

                    case CalculationType.Persent:
                        double caloriesFromPercentage = (double)nutrient.Value! * response.Calories;
                        if (nutrient.NutrientType == NutrientType.Protein)
                            response.Proteins = caloriesFromPercentage / 4;
                        else if (nutrient.NutrientType == NutrientType.Fat)
                            response.Fats = caloriesFromPercentage / 9;
                        else if (nutrient.NutrientType == NutrientType.Carbohydrate)
                            response.Carbohydrates = caloriesFromPercentage / 4;
                        break;

                    case CalculationType.Fixed:
                        if (nutrient.NutrientType == NutrientType.Protein)
                            response.Proteins = (double)nutrient.Value!;
                        else if (nutrient.NutrientType == NutrientType.Fat)
                            response.Fats = (double)nutrient.Value!;
                        else if (nutrient.NutrientType == NutrientType.Carbohydrate)
                            response.Carbohydrates = (double)nutrient.Value!;
                        break;

                    case CalculationType.Bydefault:
                        double remainingCalories = response.Calories - (response.Proteins * 4 + response.Fats * 9 + response.Carbohydrates * 4);
                        if (remainingCalories > 0)
                        {
                            if (nutrient.NutrientType == NutrientType.Protein)
                                response.Proteins = remainingCalories / 4;
                            else if (nutrient.NutrientType == NutrientType.Fat)
                                response.Fats = remainingCalories / 9;
                            else if (nutrient.NutrientType == NutrientType.Carbohydrate)
                                response.Carbohydrates = remainingCalories / 4;
                        }
                        break;
                }
            }

            return response;
        }
    }
}
