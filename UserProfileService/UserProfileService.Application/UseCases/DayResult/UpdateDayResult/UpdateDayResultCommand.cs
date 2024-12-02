using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.DayResult
{
    public record UpdateDayResultCommand(UpdateDayResultDTO UpdateDayResultDTO, int userId)
        : IRequest<Domain.Entities.DayResult>;
}
