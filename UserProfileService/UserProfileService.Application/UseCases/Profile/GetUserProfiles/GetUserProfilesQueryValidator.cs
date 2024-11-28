using FluentValidation;

namespace UserProfileService.Application.UseCases.Profile
{
    internal class GetUserProfilesQueryValidator : AbstractValidator<GetUserProfilesQuery>
    {
        public GetUserProfilesQueryValidator() 
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("User ID must be greater than 0.");
        }
    }
}
