using MediatR;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class GetOrCreateDayResultCommand : IRequest<Domain.Entities.DayResult>
    {
        public int ProfileId { get; set; }
        public DateOnly Date { get; set; }

        public GetOrCreateDayResultCommand(int profileId, DateOnly date)
        {
            ProfileId = profileId;
            Date = date;
        }
    }
}
