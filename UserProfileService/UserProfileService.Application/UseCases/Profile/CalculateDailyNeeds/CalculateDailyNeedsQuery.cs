using MediatR;
using UserProfileService.Application.Models;

namespace UserProfileService.Application.UseCases.Profile
{
    public record CalculateDailyNeedsQuery(int ProfileId, int userId) : IRequest<DailyNeedsResponse>;
}
