using FluentValidation;
using MealPlanService.Application.Validators;

namespace MealPlanService.Application.UseCases
{
    public class UpdateMealPlanCommandValidator : AbstractValidator<UpdateMealPlanCommand>
    {
        public UpdateMealPlanCommandValidator(UpdateMealPlanDTOValidator updateMealPlanDTOValidator)
        {
            RuleFor(x => x.MealPlan)
                .NotEmpty().WithMessage("Meal plan data is required.")
                .SetValidator(updateMealPlanDTOValidator);
        }
    }
}