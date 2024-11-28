using FluentValidation;
using UserProfileService.Application.UseCases.DayResult;

namespace UserProfileService.Application.UseCases.Validators
{
    public class GetOrCreateDayResultCommandValidator : AbstractValidator<GetOrCreateDayResultCommand>
    {
        public GetOrCreateDayResultCommandValidator()
        {
            RuleFor(query => query.ProfileId)
                .GreaterThan(0).WithMessage("ProfileId must be greater than 0.");

            RuleFor(query => query.Date)
                .NotEmpty().WithMessage("Date is required.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));
        }
    }
}
