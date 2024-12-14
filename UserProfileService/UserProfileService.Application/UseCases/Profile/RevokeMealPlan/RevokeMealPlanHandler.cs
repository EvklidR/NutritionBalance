using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;
using MediatR;

namespace UserProfileService.Application.UseCases.Profile
{
    public class RevokeMealPlanHandler : IRequestHandler<RevokeMealPlanCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RevokeMealPlanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(RevokeMealPlanCommand request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.ProfileId);
            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            profile.MealPlanId = null;
            profile.DateOfStartPlan = null;

            _unitOfWork.ProfileRepository.Update(profile);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
