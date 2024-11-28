using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Profile
{
    public class AddProfileCommand : IRequest<int>
    {
        public CreateProfileDTO ProfileDto { get; }

        public AddProfileCommand(CreateProfileDTO profileDto)
        {
            ProfileDto = profileDto;
        }
    }
}
