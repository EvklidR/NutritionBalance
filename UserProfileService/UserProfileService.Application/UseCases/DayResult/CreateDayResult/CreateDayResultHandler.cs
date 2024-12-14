using AutoMapper;
using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class CreateDayResultCommandHandler : IRequestHandler<CreateDayResultCommand, Domain.Entities.DayResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDayResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.DayResult> Handle(
            CreateDayResultCommand request,
            CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.CreateDayResultDTO.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var dayResult = _mapper.Map<Domain.Entities.DayResult>(request.CreateDayResultDTO);

            _unitOfWork.DayResultRepository.Add(dayResult);
            await _unitOfWork.SaveChangesAsync();

            return dayResult;
        }
    }
}
