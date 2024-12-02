using FluentValidation;

namespace UserProfileService.Application.UseCases.Profile
{
    public class CalculateDailyNeedsQueryValidator : AbstractValidator<CalculateDailyNeedsQuery>
    {
        public CalculateDailyNeedsQueryValidator()
        {
            RuleFor(x => x.ProfileId)
                .GreaterThan(0)
                .WithMessage("Profile ID must be greater than zero.");
        }
    }
}