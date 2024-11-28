using FluentValidation;
using UserProfileService.Application.UseCases.DayResult;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Validators
{
    public class CreateDayResultCommandValidator : AbstractValidator<CreateDayResultCommand>
    {
        public CreateDayResultCommandValidator()
        {
            RuleFor(cmd => cmd.CreateDayResultDTO)
                .NotNull().WithMessage("Creation DayResult data must not be null.")
                .SetValidator(new CreateDayResultDTOValidator());

            RuleForEach(cmd => cmd.CreateDayResultDTO.Meals)
                .SetValidator(new CreateMealDTOValidator());
        }
    }
}
