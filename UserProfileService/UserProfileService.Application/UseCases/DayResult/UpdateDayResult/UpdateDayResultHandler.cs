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

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(dayResult.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            _mapper.Map(request.UpdateDayResultDTO, dayResult);

            _unitOfWork.DayResultRepository.Update(dayResult);
            await _unitOfWork.SaveChangesAsync();

            return dayResult;
        }
    }
}
