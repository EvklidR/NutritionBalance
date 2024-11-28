using MediatR;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Application.UseCases.Profile
{
    public class GetUserProfilesQuery : IRequest<IEnumerable<Domain.Entities.Profile>?>
    {
        public int UserId { get; }

        public GetUserProfilesQuery(int userId)
        {
            UserId = userId;
        }
    }
}
