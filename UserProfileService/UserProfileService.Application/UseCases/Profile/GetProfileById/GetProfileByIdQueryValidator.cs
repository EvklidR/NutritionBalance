using FluentValidation;

namespace UserProfileService.Application.UseCases.Profile
{
    public class GetProfileByIdQueryValidator : AbstractValidator<GetProfileByIdQuery>
    {
        public GetProfileByIdQueryValidator() 
        {
            RuleFor(x => x.userId)
                .GreaterThan(0);
            RuleFor(x => x.profileId)
                .GreaterThan(0);
        }
    }
}
