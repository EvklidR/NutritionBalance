using FluentValidation;
using UserProfileService.Application.UseCases.DayResult;

namespace UserProfileService.Application.UseCases.Validators
{
    public class DeleteDayResultCommandValidator : AbstractValidator<DeleteDayResultCommand>
    {
        public DeleteDayResultCommandValidator()
        {
            RuleFor(cmd => cmd.Id)
                .GreaterThan(0).WithMessage("DayResult ID must be greater than 0.");
        }
    }
}
