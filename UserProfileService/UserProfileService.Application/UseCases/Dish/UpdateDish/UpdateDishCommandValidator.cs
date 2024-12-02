using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Dish
{
    public class UpdateDishCommandValidator : AbstractValidator<UpdateDishCommand>
    {
        public UpdateDishCommandValidator(UpdateDishDTOValidator updateDishDTOValidator) 
        {
            RuleFor(x => x.Dish)
                .NotEmpty().WithMessage("Updating dish data must not be null.")
                .SetValidator(updateDishDTOValidator);
        }
    }
}
