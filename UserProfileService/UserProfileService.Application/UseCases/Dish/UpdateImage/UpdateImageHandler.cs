using UserProfileService.Application.Interfaces;
using MediatR;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

namespace UserProfileService.Application.UseCases.Dish
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
            var dish = await _unitOfWork.DishRepository.GetByIdAsync(request.dishId);
            if (dish == null)
                throw new NotFoundException("Dish not found");

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(dish.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var ImageName = await _imageService.SaveImageAsync(request.file);
            dish.ImageUrl = ImageName;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
