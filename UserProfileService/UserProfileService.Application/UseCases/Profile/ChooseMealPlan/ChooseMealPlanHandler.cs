using MediatR;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

namespace UserProfileService.Application.UseCases.Profile
{
    public class ChooseMealPlanHandler : IRequestHandler<ChooseMealPlanCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChooseMealPlanHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(ChooseMealPlanCommand request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.ProfileId);
            if (profile == null)
                throw new NotFoundException("Profile not found");

            profile.MealPlanId = request.MealPlanId;
            profile.DateOfStartPlan = DateOnly.FromDateTime(DateTime.Now);

            _unitOfWork.ProfileRepository.Update(profile);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
