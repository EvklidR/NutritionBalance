using FluentValidation;

namespace UserProfileService.Application.UseCases.Dish
{
    public class GetDishByIdQueryValidator : AbstractValidator<GetDishByIdQuery>
    {
        public GetDishByIdQueryValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
