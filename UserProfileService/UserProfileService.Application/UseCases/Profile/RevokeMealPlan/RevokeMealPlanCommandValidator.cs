using FluentValidation;

namespace UserProfileService.Application.UseCases.Profile
{
    public class RevokeMealPlanCommandValidator : AbstractValidator<RevokeMealPlanCommand>
    {
        public RevokeMealPlanCommandValidator()
        {
            RuleFor(x => x.ProfileId).GreaterThan(0);
        }
    }
}
