using FluentValidation;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class GetProductsFromAPIQueryValidator : AbstractValidator<GetProductsFromAPIQuery>
    {
        public GetProductsFromAPIQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name of product must be not empty");
        }
    }
}
