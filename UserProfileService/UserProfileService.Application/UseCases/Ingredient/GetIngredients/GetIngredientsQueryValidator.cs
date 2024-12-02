using FluentValidation;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class GetIngredientsQueryValidator : AbstractValidator<GetIngredientsQuery>
    {
        public GetIngredientsQueryValidator()
        {
            RuleFor(x => x.ProfileId)
                .GreaterThan(0).WithMessage("Profile ID must be greater than zero.");
        }
    }
}