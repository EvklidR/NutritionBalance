using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Meal
{
    public class DeleteMealHandler : IRequestHandler<DeleteMealCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMealHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteMealCommand request, CancellationToken cancellationToken)
        {
            var day = await _unitOfWork.DayResultRepository.GetByIdAsync(request.DayId);

            if (day == null) 
            {
                throw new NotFoundException("Day not found");
            }
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(day.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var meal = day.Meals.FirstOrDefault(m => m.Id == request.MealId);
            if (meal == null) 
            {
                throw new NotFoundException("Meal not found");
            }

            day.Meals.Remove(meal);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
