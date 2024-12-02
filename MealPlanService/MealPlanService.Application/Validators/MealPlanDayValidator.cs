using FluentValidation;
using MealPlanService.Application.DTOs;
using MealPlanService.Domain.Enums;

namespace MealPlanService.Application.Validators
{
    public class MealPlanDayValidator : AbstractValidator<MealPlanDayDTO>
    {
        public MealPlanDayValidator(NutrientOfDayValidator nutrientOfDayValidator)
        {
            RuleFor(d => d.NutrientsOfDay)
                .NotEmpty().WithMessage("Each day must have at least one nutrient.");

            RuleForEach(d => d.NutrientsOfDay)
                .SetValidator(nutrientOfDayValidator);

            RuleFor(d => d.NutrientsOfDay)
                .Must(nutrients => nutrients.Select(n => n.NutrientType).Distinct().Count() == 3)
                .WithMessage("Each day must have exactly 3 different nutrient types.");

            RuleFor(d => d.NutrientsOfDay)
               .Must(NotExceedOneDefaultNutrient)
               .WithMessage("Each day can have at most one nutrient with a default calculation.");

            RuleFor(d => d.NutrientsOfDay)
                .Must(HaveDefaultIfFixedOrPerKg)
                .WithMessage("If there are fixed or per-kg calculations, there must be at least one default calculation.");

            RuleFor(d => d.NutrientsOfDay)
                .Must(HaveValidPercentagesIfAllPercent)
                .WithMessage("If all nutrients are calculated as percentages, their sum must equal 1.");
        }
        private bool NotExceedOneDefaultNutrient(IEnumerable<NutrientOfDayDTO> nutrients)
        {
            return nutrients.Count(n => n.CalculationType == CalculationType.Bydefault) <= 1;
        }

        private bool HaveDefaultIfFixedOrPerKg(IEnumerable<NutrientOfDayDTO> nutrients)
        {
            bool hasFixedOrPerKg = nutrients.Any(n =>
                n.CalculationType == CalculationType.Fixed ||
                n.CalculationType == CalculationType.PerKg);

            bool hasDefault = nutrients.Any(n => n.CalculationType == CalculationType.Bydefault);

            return !hasFixedOrPerKg || hasDefault;
        }

        private bool HaveValidPercentagesIfAllPercent(IEnumerable<NutrientOfDayDTO> nutrients)
        {
            bool allArePercent = nutrients.All(n => n.CalculationType == CalculationType.Persent);

            if (!allArePercent)
                return true;

            double totalPercentage = nutrients.Sum(n => n.Value ?? 0);

            return totalPercentage == 1;
        }

    }
}
