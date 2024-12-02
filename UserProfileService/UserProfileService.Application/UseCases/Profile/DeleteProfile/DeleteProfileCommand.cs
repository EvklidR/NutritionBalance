using MediatR;

namespace UserProfileService.Application.UseCases.Profile
{
    public record DeleteProfileCommand(int ProfileId, int userId) : IRequest;
}
