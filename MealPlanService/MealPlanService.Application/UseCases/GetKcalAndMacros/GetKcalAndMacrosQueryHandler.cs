using MediatR;
using MealPlanService.Domain.Interfaces;
using MealPlanService.Domain.Enums;
using MealPlanService.Application.Exceptions;

namespace MealPlanService.Application.UseCases
{
    public class GetKcalAndMacrosQueryHandler : IRequestHandler<GetKcalAndMacrosQuery, (double Calories, double Protein, double Fat, double Carbs)>
    {
        private readonly IMealPlanRepository _mealPlanRepository;

        public GetKcalAndMacrosQueryHandler(IMealPlanRepository mealPlanRepository)
        {
            _mealPlanRepository = mealPlanRepository;
        }

        public async Task<(double Calories, double Protein, double Fat, double Carbs)> Handle(GetKcalAndMacrosQuery request, CancellationToken cancellationToken)
        {
            var mealPlan = await _mealPlanRepository.GetByIdAsync(request.mealPlanId);
            if (mealPlan == null)
                throw new NotFoundException("Meal plan not found");

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            int daysPassed = currentDate.DayNumber - request.startDate.DayNumber;

            var mealPlanDay = mealPlan.Days.FirstOrDefault(d => d.NumberOfDay == daysPassed/mealPlan.Days.Count + 1);
            if (mealPlanDay == null)
                throw new NotFoundException("Meal plan day not found");

            double totalCalories = mealPlanDay.CaloriePercentage * request.daylyKcal;

            double protein = 0, fat = 0, carbs = 0;

            foreach(var nutrient in mealPlanDay.NutrientsOfDay) 
            {
                switch (nutrient.CalculationType)
                {
                    case CalculationType.PerKg:
                        if (nutrient.NutrientType == NutrientType.Protein)
                            protein = (double)nutrient.Value! * request.bodyWeight;
                        else if (nutrient.NutrientType == NutrientType.Fat)
                            fat = (double)nutrient.Value! * request.bodyWeight;
                        else if (nutrient.NutrientType == NutrientType.Carbohydrate)
                            carbs = (double)nutrient.Value! * request.bodyWeight;
                        break;

                    case CalculationType.Persent:
                        double caloriesFromPercentage = (double)nutrient.Value! * totalCalories;
                        if (nutrient.NutrientType == NutrientType.Protein)
                            protein = caloriesFromPercentage / 4;
                        else if (nutrient.NutrientType == NutrientType.Fat)
                            fat = caloriesFromPercentage / 9;
                        else if (nutrient.NutrientType == NutrientType.Carbohydrate)
                            carbs = caloriesFromPercentage / 4;
                        break;

                    case CalculationType.Fixed:
                        if (nutrient.NutrientType == NutrientType.Protein)
                            protein = (double)nutrient.Value!;
                        else if (nutrient.NutrientType == NutrientType.Fat)
                            fat = (double)nutrient.Value!;
                        else if (nutrient.NutrientType == NutrientType.Carbohydrate)
                            carbs = (double)nutrient.Value!;
                        break;

                    case CalculationType.Bydefault:
                        double remainingCalories = totalCalories - (protein * 4 + fat * 9 + carbs * 4);
                        if (remainingCalories > 0)
                        {
                            if (nutrient.NutrientType == NutrientType.Protein)
                                protein = remainingCalories / 4;
                            else if (nutrient.NutrientType == NutrientType.Fat)
                                fat = remainingCalories / 9;
                            else if (nutrient.NutrientType == NutrientType.Carbohydrate)
                                carbs = remainingCalories / 4;
                        }
                        break;
                }
            }

            return (totalCalories, protein, fat, carbs);
        }
    }
}
