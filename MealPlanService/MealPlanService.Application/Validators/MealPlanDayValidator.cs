using FluentValidation;
using MealPlanService.Domain.Entities;
using System.Linq;

namespace MealPlanService.Application.Validators
{
    public class MealPlanDayValidator : AbstractValidator<MealPlanDay>
    {
        public MealPlanDayValidator()
        {
            RuleFor(d => d.NutrientsOfDay)
                .NotEmpty().WithMessage("Each day must have at least one nutrient.");

            RuleForEach(d => d.NutrientsOfDay)
                .SetValidator(new NutrientOfDayValidator());

            RuleFor(d => d.NutrientsOfDay)
                .Must(nutrients => nutrients.Select(n => n.NutrientType).Distinct().Count() == 3)
                .WithMessage("Each day must have exactly 3 different nutrient types.");
        }
    }
}
