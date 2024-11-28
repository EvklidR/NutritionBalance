using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class CreateDayResultCommand : IRequest<Domain.Entities.DayResult>
    {
        public CreateDayResultDTO CreateDayResultDTO { get; set; }

        public CreateDayResultCommand(CreateDayResultDTO createDayResultDTO)
        {
            CreateDayResultDTO = createDayResultDTO;
        }
    }
}
