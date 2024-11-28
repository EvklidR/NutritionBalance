using FluentValidation;
using UserProfileService.Application.UseCases.DayResult;

namespace UserProfileService.Application.UseCases.Validators
{
    public class GetDayResultByIdQueryValidator : AbstractValidator<GetDayResultByIdQuery>
    {
        public GetDayResultByIdQueryValidator()
        {
            RuleFor(query => query.Id)
                .GreaterThan(0).WithMessage("DayResult ID must be greater than 0.");
        }
    }
}
