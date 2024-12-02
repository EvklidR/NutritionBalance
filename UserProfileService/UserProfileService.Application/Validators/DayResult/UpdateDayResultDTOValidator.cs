using FluentValidation;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.Validators
{
    public class UpdateDayResultDTOValidator : AbstractValidator<UpdateDayResultDTO>
    {
        public UpdateDayResultDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Day id must be grater than 0");
            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be grater than 0 if provided")
                .When(x => x.Weight.HasValue);
            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Height must be grater than 0 if provided")
                .When(x => x.Height.HasValue);
            RuleFor(x => x.ActivityLevel)
                .IsInEnum().WithMessage("ActivityLevel should be in enum range");
            RuleFor(x => x.GlassesOfWater)
                .GreaterThan(0).WithMessage("Amount should be grater than 0");
        }
    }
}
