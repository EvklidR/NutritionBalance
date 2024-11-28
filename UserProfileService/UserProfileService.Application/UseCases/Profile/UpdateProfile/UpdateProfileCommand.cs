using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Profile
{
    public class UpdateProfileCommand : IRequest
    {
        public UpdateProfileDTO ProfileDto { get; }

        public UpdateProfileCommand(UpdateProfileDTO profileDto)
        {
            ProfileDto = profileDto;
        }
    }
}
