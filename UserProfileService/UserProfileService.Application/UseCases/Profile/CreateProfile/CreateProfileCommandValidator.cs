using FluentValidation;
using UserProfileService.Application.Validators;

namespace UserProfileService.Application.UseCases.Profile
{
    public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileCommandValidator(CreateProfileDTOValidator createProfileDTOValidator)
        {
            RuleFor(x => x.ProfileDto)
                .NotNull()
                .WithMessage("Profile data is required.")
                .SetValidator(createProfileDTOValidator);
        }
    }
}