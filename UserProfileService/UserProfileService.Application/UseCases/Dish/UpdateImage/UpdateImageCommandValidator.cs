using FluentValidation;

namespace UserProfileService.Application.UseCases.Dish
{
    public class UpdateImageCommandValidator : AbstractValidator<UpdateImageCommand>
    {
        public UpdateImageCommandValidator() 
        {
            RuleFor(x => x.file)
                .NotEmpty().WithMessage("File is required.");
            RuleFor(x => x.dishId)
                .GreaterThan(0).WithMessage("Meal plan ID must be greater than zero.");
        }
    }
}
