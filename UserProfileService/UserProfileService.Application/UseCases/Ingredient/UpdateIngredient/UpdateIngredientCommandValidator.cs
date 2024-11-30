using FluentValidation;
using UserProfileService.Application.Validators;
using UserProfileService.Application.UseCases.Ingredient.UpdateIngredient;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class UpdateIngredientCommandValidator : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientCommandValidator() 
        {
            RuleFor(x => x.IngredientDTO).NotEmpty()
                .SetValidator(new UpdateIngredientDTOValidator());
        }
    }
}
