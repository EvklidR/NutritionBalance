using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class GetOrCreateDayResultQueryHandler 
        : IRequestHandler<GetOrCreateDayResultCommand, Domain.Entities.DayResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrCreateDayResultQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.DayResult> Handle(
            GetOrCreateDayResultCommand request,
            CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var dayResult = await _unitOfWork.DayResultRepository
                .GetByDateAsync(request.ProfileId, request.Date);

            if (dayResult == null)
            {
                dayResult = new Domain.Entities.DayResult
                {
                    ProfileId = request.ProfileId,
                    Date = request.Date,
                    GlassesOfWater = 0
                };

                _unitOfWork.DayResultRepository.Add(dayResult);
                await _unitOfWork.SaveChangesAsync();
            }

            return dayResult;
        }
    }
}