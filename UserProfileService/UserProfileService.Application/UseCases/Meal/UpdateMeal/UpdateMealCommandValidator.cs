using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Meal
{
    public class UpdateMealCommandValidator : AbstractValidator<UpdateMealCommand>
    {
        public UpdateMealCommandValidator(UpdateMealDTOValidator mealDTOValidator) 
        {
            RuleFor(X => X.UpdateMealDTO)
                .NotEmpty().WithMessage("Upadating data must not be empty")
                .SetValidator(mealDTOValidator);
        }
    }
}
