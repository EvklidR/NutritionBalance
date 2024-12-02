using MediatR;

namespace UserProfileService.Application.UseCases.DayResult
{
    public record GetOrCreateDayResultCommand(int ProfileId, DateOnly Date, int userId)
        : IRequest<Domain.Entities.DayResult>;
}
