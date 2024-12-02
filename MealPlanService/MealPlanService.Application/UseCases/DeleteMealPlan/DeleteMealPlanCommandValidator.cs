using FluentValidation;

namespace MealPlanService.Application.UseCases
{
    public class DeleteMealPlanCommandValidator : AbstractValidator<DeleteMealPlanCommand>
    {
        public DeleteMealPlanCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Meal plan ID must be greater than zero.");
        }
    }
}