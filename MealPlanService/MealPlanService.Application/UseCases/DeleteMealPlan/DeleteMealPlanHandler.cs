using MediatR;
using MealPlanService.Domain.Interfaces;
using MealPlanService.Application.Exceptions;
using MealPlanService.Application.Interfaces;

namespace MealPlanService.Application.UseCases
{
    public class DeleteMealPlanHandler : IRequestHandler<DeleteMealPlanCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public DeleteMealPlanHandler(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public async Task Handle(DeleteMealPlanCommand request, CancellationToken cancellationToken)
        {
            var mealPlan = await _unitOfWork.MealPlans.GetByIdAsync(request.Id);

            if (mealPlan == null)
            {
                throw new NotFoundException("Meal plan with this id not found");
            }

            if (request.userId != mealPlan.OwnerId)
                throw new UnauthorizedException("Owner isn't valid");

            if (!string.IsNullOrEmpty(mealPlan.ImageUrl))
            {
                _imageService.DeleteImage(mealPlan.ImageUrl);
            }

            _unitOfWork.MealPlans.Delete(mealPlan);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
