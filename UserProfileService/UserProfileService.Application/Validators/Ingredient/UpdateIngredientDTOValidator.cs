using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class UpdateIngredientDTOValidator : AbstractValidator<UpdateIngredientDTO>
    {
        public UpdateIngredientDTOValidator() 
        {
            RuleFor(i => i.Name).NotEmpty();
            RuleFor(i => i.Proteins).GreaterThanOrEqualTo(0);
            RuleFor(i => i.Fats).GreaterThanOrEqualTo(0);
            RuleFor(i => i.Carbohydrates).GreaterThanOrEqualTo(0);
        }
    }
}
