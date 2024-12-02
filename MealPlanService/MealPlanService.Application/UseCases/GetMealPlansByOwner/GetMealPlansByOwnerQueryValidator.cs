using FluentValidation;

namespace MealPlanService.Application.UseCases
{
    public class GetMealPlansByOwnerQueryValidator : AbstractValidator<GetMealPlansByOwnerQuery>
    {
        public GetMealPlansByOwnerQueryValidator() 
        {
            RuleFor(x => x.ownerId)
                .GreaterThan(0).WithMessage("Owner id must be grater than 0");
        }
    }
}
