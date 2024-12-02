using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Profile
{
    public record CreateProfileCommand(CreateProfileDTO ProfileDto) : IRequest<Domain.Entities.Profile>;
}
