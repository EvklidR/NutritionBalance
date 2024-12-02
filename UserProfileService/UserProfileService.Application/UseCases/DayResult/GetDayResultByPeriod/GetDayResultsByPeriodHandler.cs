using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class GetDayResultsByPeriodQueryHandler : IRequestHandler<GetDayResultsByPeriodQuery, IEnumerable<Domain.Entities.DayResult>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDayResultsByPeriodQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Entities.DayResult>?> Handle(GetDayResultsByPeriodQuery request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            return await _unitOfWork.DayResultRepository.GetAllByPeriodAsync(request.ProfileId, request.StartDate, request.EndDate);
        }
    }
}
