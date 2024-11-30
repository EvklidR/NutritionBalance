using AutoMapper;
using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class UpdateDayResultHandler : IRequestHandler<UpdateDayResultCommand, Domain.Entities.DayResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDayResultHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.DayResult> Handle(UpdateDayResultCommand request, CancellationToken cancellationToken)
        {
            var dayResult = await _unitOfWork.DayResultRepository.GetByIdAsync(request.UpdateDayResultDTO.Id);

            if (dayResult == null)
                throw new NotFoundException($"DayResult with Id {request.UpdateDayResultDTO.Id} not found.");

            _mapper.Map(request.UpdateDayResultDTO, dayResult);

            _unitOfWork.DayResultRepository.Update(dayResult);
            await _unitOfWork.SaveChangesAsync();

            return dayResult;
        }
    }
}
