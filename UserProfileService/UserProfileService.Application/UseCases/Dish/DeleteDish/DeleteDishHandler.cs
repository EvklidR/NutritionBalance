using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Application.Interfaces;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Dish
{
    public class DeleteDishHandler : IRequestHandler<DeleteDishCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public DeleteDishHandler(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _unitOfWork.DishRepository.GetByIdAsync(request.Id);
            if (dish == null)
                throw new NotFoundException("Dish not found");

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(dish.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            if (!string.IsNullOrEmpty(dish.ImageUrl))
            {
                _imageService.DeleteImage(dish.ImageUrl);
            }

            _unitOfWork.DishRepository.Delete(dish);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
