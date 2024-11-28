using MediatR;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class GetDayResultByIdQuery : IRequest<Domain.Entities.DayResult>
    {
        public int Id { get; set; }

        public GetDayResultByIdQuery(int id)
        { this.Id = id; }
    }
}
