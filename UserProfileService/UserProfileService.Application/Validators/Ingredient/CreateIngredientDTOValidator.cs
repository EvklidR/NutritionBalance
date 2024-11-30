using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class CreateIngredientDTOValidator : AbstractValidator<CreateIngredientDTO>
    {
        public CreateIngredientDTOValidator() 
        {
            RuleFor(x => x.Proteins)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Fats)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Carbohydrates)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Name)
                .NotNull();

            RuleFor(x => x.ProfileId)
                .GreaterThan(0);
        }
    }
}
