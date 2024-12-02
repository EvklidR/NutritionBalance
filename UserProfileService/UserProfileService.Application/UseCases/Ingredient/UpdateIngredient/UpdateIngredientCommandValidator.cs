using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class UpdateIngredientCommandValidator : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientCommandValidator(UpdateIngredientDTOValidator updateIngredientDTOValidator)
        {
            RuleFor(x => x.IngredientDTO)
                .NotEmpty().WithMessage("Ingredient data is required.")
                .SetValidator(updateIngredientDTOValidator);
        }
    }
}