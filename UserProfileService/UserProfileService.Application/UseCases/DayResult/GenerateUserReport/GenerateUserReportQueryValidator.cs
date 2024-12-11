using FluentValidation;
using UserProfileService.Application.UseCases.DayResult;

namespace UserProfileService.Application.UseCases.Validators
{
    public class GenerateUserReportQueryValidator : AbstractValidator<GenerateUserReportQuery>
    {
        public GenerateUserReportQueryValidator()
        {
            RuleFor(query => query.profileId)
                .GreaterThan(0).WithMessage("ProfileId must be greater than 0.");

            RuleFor(query => query.startDate)
                .NotEmpty().WithMessage("StartDate is required.");

            RuleFor(query => query.endDate)
                .NotEmpty().WithMessage("EndDate is required.")
                .GreaterThan(query => query.startDate)
                .WithMessage("EndDate must be greater than StartDate.");
        }
    }
}
