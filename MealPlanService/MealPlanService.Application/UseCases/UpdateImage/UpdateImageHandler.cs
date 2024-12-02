using MealPlanService.Application.Interfaces;
using MediatR;
using MealPlanService.Domain.Interfaces;
using MealPlanService.Application.Exceptions;

namespace MealPlanService.Application.UseCases.UpdateImage
{
    public class UpdateImageHandler : IRequestHandler<UpdateImageCommand>
    {
        private readonly IImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateImageHandler(IImageService imageService, IUnitOfWork unitOfWork)
        {
            _imageService = imageService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            var mealPlan = await _unitOfWork.MealPlans.GetByIdAsync(request.mealPlanId);
            if (mealPlan == null)
                throw new NotFoundException("Meal plan not found");

            if (request.userId != mealPlan.OwnerId)
                throw new UnauthorizedException("Owner isn't valid");

            var ImageName = await _imageService.SaveImageAsync(request.file);
            mealPlan.ImageUrl = ImageName;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
