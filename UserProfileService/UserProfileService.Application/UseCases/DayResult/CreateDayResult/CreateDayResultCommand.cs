using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.DayResult
{
    public record CreateDayResultCommand(CreateDayResultDTO CreateDayResultDTO, int userId) : IRequest<Domain.Entities.DayResult>;
}
