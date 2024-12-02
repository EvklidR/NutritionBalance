using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Profile
{
    public record UpdateProfileCommand(UpdateProfileDTO ProfileDto, int userId) : IRequest;
}
