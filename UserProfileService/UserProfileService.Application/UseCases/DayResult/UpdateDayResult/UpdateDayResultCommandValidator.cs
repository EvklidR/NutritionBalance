using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class UpdateDayResultCommandValidator : AbstractValidator<UpdateDayResultCommand>
    {
        public UpdateDayResultCommandValidator(UpdateDayResultDTOValidator updateDayResultDTOValidator) 
        {
            RuleFor(x => x.UpdateDayResultDTO)
                .NotEmpty()
                .SetValidator(updateDayResultDTOValidator);
        }
    }
}
