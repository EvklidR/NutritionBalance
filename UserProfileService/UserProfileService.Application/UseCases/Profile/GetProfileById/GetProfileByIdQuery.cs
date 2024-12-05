using MediatR;

namespace UserProfileService.Application.UseCases.Profile
{
    public record GetProfileByIdQuery(int profileId, int userId) : IRequest<Domain.Entities.Profile>;
}
