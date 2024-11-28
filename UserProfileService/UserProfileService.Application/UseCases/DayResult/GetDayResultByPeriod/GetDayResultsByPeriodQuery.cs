using MediatR;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class GetDayResultsByPeriodQuery : IRequest<IEnumerable<Domain.Entities.DayResult>?>
    {
        public int ProfileId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public GetDayResultsByPeriodQuery(int profileId, DateOnly startDate, DateOnly endDate) 
        {
            ProfileId = profileId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
