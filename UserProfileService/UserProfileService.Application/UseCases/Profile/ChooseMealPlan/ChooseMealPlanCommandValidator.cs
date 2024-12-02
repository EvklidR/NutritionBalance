using FluentValidation;

namespace UserProfileService.Application.UseCases.Profile
{
    public class ChooseMealPlanCommandValidator : AbstractValidator<ChooseMealPlanCommand>
    {
        public ChooseMealPlanCommandValidator()
        {
            RuleFor(x => x.MealPlanId)
                .GreaterThan(0)
                .WithMessage("Meal Plan ID must be greater than zero.");

            RuleFor(x => x.ProfileId)
                .GreaterThan(0)
                .WithMessage("Profile ID must be greater than zero.");
        }
    }
}