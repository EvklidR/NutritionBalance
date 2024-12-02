using MediatR;

namespace UserProfileService.Application.UseCases.Profile
{
    public record GetUserProfilesQuery(int UserId) : IRequest<IEnumerable<Domain.Entities.Profile>?>;
}
