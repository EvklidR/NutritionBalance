using MediatR;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

namespace UserProfileService.Application.UseCases.DayResult
{
    public class DeleteDayResultCommandHandler : IRequestHandler<DeleteDayResultCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDayResultCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteDayResultCommand request, CancellationToken cancellationToken)
        {
            var dayResult = await _unitOfWork.DayResultRepository.GetByIdAsync(request.Id);
            if (dayResult == null)
                throw new NotFoundException("DayResult not found");

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(dayResult.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            _unitOfWork.DayResultRepository.Delete(dayResult);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
