using FluentValidation;

namespace MealPlanService.Application.UseCases.UpdateImage
{
    public class UpdateImageCommandValidator : AbstractValidator<UpdateImageCommand>
    {
        public UpdateImageCommandValidator()
        {
            RuleFor(x => x.file)
                .NotEmpty().WithMessage("File is required.");
            RuleFor(x => x.mealPlanId)
                .GreaterThan(0).WithMessage("Meal plan ID must be greater than zero.");
        }
    }
}