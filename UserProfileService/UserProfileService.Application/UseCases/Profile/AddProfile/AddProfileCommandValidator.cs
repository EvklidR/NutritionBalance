using FluentValidation;
using UserProfileService.Application.Validators;
namespace UserProfileService.Application.UseCases.Profile
{
    public class AddProfileCommandValidator : AbstractValidator<AddProfileCommand>
    {
        public AddProfileCommandValidator()
        {
            RuleFor(x => x.ProfileDto)
                .NotNull()
                .SetValidator(new CreateProfileDTOValidator());
        }
    }
}
