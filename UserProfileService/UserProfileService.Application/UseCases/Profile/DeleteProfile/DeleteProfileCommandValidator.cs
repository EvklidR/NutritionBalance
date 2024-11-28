using FluentValidation;

namespace UserProfileService.Application.UseCases.Profile
{
    public class DeleteProfileCommandValidator : AbstractValidator<DeleteProfileCommand>
    {
        public DeleteProfileCommandValidator()
        {
            RuleFor(x => x.ProfileId).GreaterThan(0).WithMessage("Profile ID must be greater than 0.");
        }
    }
}
