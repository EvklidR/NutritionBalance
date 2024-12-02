using FluentValidation;
using MealPlanService.Application.Validators;

namespace MealPlanService.Application.UseCases
{
    public class CreateMealPlanCommandValidator : AbstractValidator<CreateMealPlanCommand>
    {
        public CreateMealPlanCommandValidator(CreateMealPlanValidator mealPlanValidator)
        {
            RuleFor(x => x.MealPlanDto)
                .NotEmpty().WithMessage("Meal plan data is required.")
                .SetValidator(mealPlanValidator);
        }
    }
}