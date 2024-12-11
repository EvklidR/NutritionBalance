using MediatR;

namespace UserProfileService.Application.UseCases.DayResult
{
    public record GenerateUserReportQuery(int profileId, DateOnly startDate, DateOnly endDate) : IRequest<MemoryStream>;
}
