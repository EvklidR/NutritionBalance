using MediatR;

namespace UserProfileService.Application.UseCases.DayResult
{
    public record DeleteDayResultCommand(int Id, int userId) : IRequest;
}
