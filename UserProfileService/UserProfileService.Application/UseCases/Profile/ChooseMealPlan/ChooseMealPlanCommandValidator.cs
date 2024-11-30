using FluentValidation;

namespace UserProfileService.Application.UseCases.Profile
{
    public class ChooseMealPlanCommandValidator : AbstractValidator<ChooseMealPlanCommand>
    {
        public ChooseMealPlanCommandValidator() 
        {
            RuleFor(x => x.MealPlanId).GreaterThan(0);
            RuleFor(x => x.ProfileId).GreaterThan(0);
        }
    }
}
