using MediatR;

namespace UserProfileService.Application.UseCases.DayResult
{
    public record GetDayResultsByPeriodQuery(int ProfileId, DateOnly StartDate, DateOnly EndDate, int userId)
        : IRequest<IEnumerable<Domain.Entities.DayResult>?>;
}
