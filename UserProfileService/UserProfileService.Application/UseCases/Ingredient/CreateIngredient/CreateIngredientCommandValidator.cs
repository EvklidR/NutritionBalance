using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator() 
        {
            RuleFor(x => x.IngredientDTO)
                .NotNull()
                .SetValidator(new CreateIngredientDTOValidator());
        }
    }
}
