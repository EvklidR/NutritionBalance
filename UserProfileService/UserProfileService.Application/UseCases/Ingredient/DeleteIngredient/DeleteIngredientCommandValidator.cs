using FluentValidation;

namespace UserProfileService.Application.UseCases.Ingredient.DeleteIngredient
{
    public class DeleteIngredientCommandValidator : AbstractValidator<DeleteIngredientCommand>
    {
        public DeleteIngredientCommandValidator()
        {
            RuleFor(x => x.IngredientId)
                .GreaterThan(0).WithMessage("Ingredient ID must be greater than zero.");
        }
    }
}