using MediatR;

namespace UserProfileService.Application.UseCases.DayResult
{
    public record GetDayResultByIdQuery(int Id, int userId) : IRequest<Domain.Entities.DayResult>;
}
