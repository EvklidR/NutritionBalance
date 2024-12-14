using MediatR;
using AutoMapper;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

namespace UserProfileService.Application.UseCases.Dish
{
    public class UpdateDishHandler : IRequestHandler<UpdateDishCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDishHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _unitOfWork.DishRepository.GetByIdAsync(request.Dish.Id);
            if (dish == null)
                throw new NotFoundException("Dish not found");

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(dish.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            _mapper.Map(request.Dish, dish);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
