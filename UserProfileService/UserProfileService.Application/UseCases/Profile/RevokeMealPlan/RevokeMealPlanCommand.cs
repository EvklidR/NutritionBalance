using MediatR;

namespace UserProfileService.Application.UseCases.Profile
{
    public record RevokeMealPlanCommand(int ProfileId, int userId) : IRequest;
}
