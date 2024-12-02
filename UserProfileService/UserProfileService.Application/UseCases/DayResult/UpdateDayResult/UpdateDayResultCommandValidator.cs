using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class UpdateDayResultCommandValidator : AbstractValidator<UpdateDayResultCommand>
    {
        public UpdateDayResultCommandValidator(UpdateDayResultDTOValidator updateDayResultDTOValidator) 
        {
            RuleFor(x => x.UpdateDayResultDTO)
                .NotEmpty().WithMessage("Updating DayResult data must not be null.")
                .SetValidator(updateDayResultDTOValidator);
        }
    }
}
