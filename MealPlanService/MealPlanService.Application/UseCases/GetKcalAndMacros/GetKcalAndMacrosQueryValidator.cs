using FluentValidation;

namespace MealPlanService.Application.UseCases
{
    public class GetKcalAndMacrosQueryValidator : AbstractValidator<GetKcalAndMacrosQuery>
    {
        public GetKcalAndMacrosQueryValidator()
        {
            RuleFor(x => x.daylyKcal)
                .GreaterThan(0).WithMessage("Daily Kcal must be greater than zero.");
            RuleFor(x => x.mealPlanId)
                .GreaterThan(0).WithMessage("Meal plan ID must be greater than zero.");
            RuleFor(x => x.bodyWeight)
                .GreaterThan(0).WithMessage("Body weight must be greater than zero.");
            RuleFor(x => x.startDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Start date should be in the past or today.");
        }
    }
}