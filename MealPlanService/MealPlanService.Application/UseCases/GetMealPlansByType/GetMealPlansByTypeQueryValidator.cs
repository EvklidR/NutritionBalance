using FluentValidation;

namespace MealPlanService.Application.UseCases
{
    public class GetMealPlansByTypeQueryValidator : AbstractValidator<GetMealPlansByTypeQuery>
    {
        public GetMealPlansByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid meal plan type.");
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than zero.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than zero.");
        }
    }
}