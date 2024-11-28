using MediatR;
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
            return await _unitOfWork.DayResultRepository.GetAllByPeriodAsync(request.ProfileId, request.StartDate, request.EndDate);
        }
    }
}
