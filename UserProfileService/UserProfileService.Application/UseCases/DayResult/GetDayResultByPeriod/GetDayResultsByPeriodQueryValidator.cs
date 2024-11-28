using FluentValidation;
using UserProfileService.Application.UseCases.DayResult;

namespace UserProfileService.Application.UseCases.Validators
{
    public class GetDayResultsByPeriodQueryValidator : AbstractValidator<GetDayResultsByPeriodQuery>
    {
        public GetDayResultsByPeriodQueryValidator()
        {
            RuleFor(query => query.ProfileId)
                .GreaterThan(0).WithMessage("ProfileId must be greater than 0.");

            RuleFor(query => query.StartDate)
                .NotEmpty().WithMessage("StartDate is required.");

            RuleFor(query => query.EndDate)
                .NotEmpty().WithMessage("EndDate is required.")
                .GreaterThan(query => query.StartDate)
                .WithMessage("EndDate must be greater than StartDate.");
        }
    }
}
