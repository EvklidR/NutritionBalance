using MediatR;

namespace UserProfileService.Application.UseCases.Profile
{
    public class DeleteProfileCommand : IRequest
    {
        public int ProfileId { get; }

        public DeleteProfileCommand(int profileId)
        {
            ProfileId = profileId;
        }
    }
}
