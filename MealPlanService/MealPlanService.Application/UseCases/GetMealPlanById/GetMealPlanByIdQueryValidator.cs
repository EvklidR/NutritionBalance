using FluentValidation;

namespace MealPlanService.Application.UseCases
{
    public class GetMealPlanByIdQueryValidator : AbstractValidator<GetMealPlanByIdQuery>
    {
        public GetMealPlanByIdQueryValidator() 
        {
            RuleFor(x => x.mealPlanId)
                .GreaterThan(0).WithMessage("Meal id must be grater than 0");
        }
    }
}
