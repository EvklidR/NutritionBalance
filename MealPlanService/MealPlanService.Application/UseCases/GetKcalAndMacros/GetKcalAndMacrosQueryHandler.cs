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

            var bodyWeightNutrients = mealPlanDay.NutrientsOfDay.Where(n => n.CalculationType == CalculationType.PerKg).ToList();
            foreach (var nutrient in bodyWeightNutrients)
            {
                double multiplier = nutrient.Value ?? 0;
                if (nutrient.NutrientType == NutrientType.Protein)
                    protein = multiplier * request.bodyWeight;
                if (nutrient.NutrientType == NutrientType.Fat)
                    fat = multiplier * request.bodyWeight;
                if (nutrient.NutrientType == NutrientType.Carbohydrate)
                    carbs = multiplier * request.bodyWeight;
            }

            var calorieBasedNutrients = mealPlanDay.NutrientsOfDay.Where(n => n.CalculationType == CalculationType.Persent).ToList();
            foreach (var nutrient in calorieBasedNutrients)
            {
                double nutrientPercentage = nutrient.Value ?? 0;
                if (nutrient.NutrientType == NutrientType.Protein)
                    protein = (nutrientPercentage / 100) * totalCalories / 4;
                if (nutrient.NutrientType == NutrientType.Fat)
                    fat = (nutrientPercentage / 100) * totalCalories / 9;
                if (nutrient.NutrientType == NutrientType.Carbohydrate)
                    carbs = (nutrientPercentage / 100) * totalCalories / 4;
            }

            var fixedNutrients = mealPlanDay.NutrientsOfDay.Where(n => n.CalculationType == CalculationType.Fixed).ToList();
            foreach (var nutrient in fixedNutrients)
            {
                double fixedAmount = nutrient.Value ?? 0;
                if (nutrient.NutrientType == NutrientType.Protein)
                    protein = fixedAmount;
                if (nutrient.NutrientType == NutrientType.Fat)
                    fat = fixedAmount;
                if (nutrient.NutrientType == NutrientType.Carbohydrate)
                    carbs = fixedAmount;
            }

            double remainingCalories = totalCalories - (protein * 4 + fat * 9 + carbs * 4);
            if (remainingCalories > 0)
            {
                var remainingNutrient = mealPlanDay.NutrientsOfDay
                    .FirstOrDefault(n => n.CalculationType == CalculationType.Bydefault); // Предполагаем, что есть только один такой нутриент

                if (remainingNutrient != null)
                {
                    double nutrientPercentage = remainingNutrient.Value ?? 0;

                    if (remainingNutrient.NutrientType == NutrientType.Protein)
                        protein = (nutrientPercentage / 100) * remainingCalories / 4;
                    else if (remainingNutrient.NutrientType == NutrientType.Fat)
                        fat = (nutrientPercentage / 100) * remainingCalories / 9;
                    else if (remainingNutrient.NutrientType == NutrientType.Carbohydrate)
                        carbs = (nutrientPercentage / 100) * remainingCalories / 4;
                }
            }



            return (Calories: totalCalories, Protein: protein, Fat: fat, Carbs: carbs);
        }
    }
}
