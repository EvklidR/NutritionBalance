using FluentValidation;
using UserProfileService.Application.UseCases.DayResult;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Validators
{
    public class CreateDayResultCommandValidator : AbstractValidator<CreateDayResultCommand>
    {
        public CreateDayResultCommandValidator(CreateDayResultDTOValidator createDayResultDTOValidator)
        {
            RuleFor(cmd => cmd.CreateDayResultDTO)
                .NotNull().WithMessage("Creation DayResult data must not be null.")
                .SetValidator(createDayResultDTOValidator);
        }
    }
}
