using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Dish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator() 
        {
            RuleFor(x => x.Dish)
                .NotEmpty()
                .SetValidator(new CreateDishDTOValidator());
        }
    }
}
