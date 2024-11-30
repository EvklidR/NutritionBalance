using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class IngredientOfDishDTOValidator : AbstractValidator<IngredientOfDishDTO>
    {
        public IngredientOfDishDTOValidator()
        {
            RuleFor(i => i.IngredientId)
                .GreaterThan(0).WithMessage("IngredientId must be greater than zero.");

            RuleFor(i => i.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than zero.");
        }
    }
}
