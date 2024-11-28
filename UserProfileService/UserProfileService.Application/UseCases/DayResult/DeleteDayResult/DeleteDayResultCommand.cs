using MediatR;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class DeleteDayResultCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteDayResultCommand(int id)
        { this.Id = id; }   
    }
}
