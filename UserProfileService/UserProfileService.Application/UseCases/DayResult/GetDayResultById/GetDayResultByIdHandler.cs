using MediatR;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class GetDayResultByIdQueryHandler 
        : IRequestHandler<GetDayResultByIdQuery, Domain.Entities.DayResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDayResultByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.DayResult> Handle(
            GetDayResultByIdQuery request,
            CancellationToken cancellationToken)
        {
            var dayResult = await _unitOfWork.DayResultRepository.GetByIdAsync(request.Id);
            if (dayResult == null)
                throw new NotFoundException("DayResult not found");

            return dayResult;
        }
    }
}
