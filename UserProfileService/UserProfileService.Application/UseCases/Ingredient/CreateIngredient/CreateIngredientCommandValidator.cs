using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator(CreateIngredientDTOValidator createIngredientDTOValidator)
        {
            RuleFor(x => x.IngredientDTO)
                .NotNull().WithMessage("Ingredient data is required.")
                .SetValidator(createIngredientDTOValidator);
        }
    }
}