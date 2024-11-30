using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class UpdateDayResultCommand : IRequest<Domain.Entities.DayResult>
    {
        public UpdateDayResultDTO UpdateDayResultDTO { get; set; }

        public UpdateDayResultCommand(UpdateDayResultDTO updateDayResultDTO)
        {
            UpdateDayResultDTO = updateDayResultDTO;
        }
    }
}
