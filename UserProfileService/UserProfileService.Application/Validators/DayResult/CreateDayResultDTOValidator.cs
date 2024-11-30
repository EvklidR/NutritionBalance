using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class CreateDayResultDTOValidator : AbstractValidator<CreateDayResultDTO>
    {
        public CreateDayResultDTOValidator()
        {
            RuleFor(dr => dr.ProfileId)
                .GreaterThan(0).WithMessage("ProfileId must be greater than 0.");

            RuleFor(dr => dr.Date)
                .NotEmpty().WithMessage("Date is required.")
                .Must(d => d < DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("The date must be in the past.");

            RuleForEach(dr => dr.Meals).SetValidator(new CreateMealDTOValidator());
        }
    }
}
