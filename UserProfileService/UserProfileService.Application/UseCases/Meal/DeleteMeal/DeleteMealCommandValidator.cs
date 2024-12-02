using FluentValidation;

namespace UserProfileService.Application.UseCases.Meal.DeleteMeal
{
    public class DeleteMealCommandValidator : AbstractValidator<DeleteMealCommand>
    {
        public DeleteMealCommandValidator()
        {
            RuleFor(x => x.MealId)
                .GreaterThan(0).WithMessage("Meal is must be grater than 0");
            RuleFor(x => x.DayId)
                .GreaterThan(0).WithMessage("Day is must be grater than 0");
        }
    }
}
