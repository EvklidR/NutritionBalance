using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Dish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator(CreateDishDTOValidator createDishDTOValidator) 
        {
            RuleFor(x => x.Dish)
                .NotEmpty().WithMessage("Creation dish data must not be null.")
                .SetValidator(createDishDTOValidator);
        }
    }
}
