using MediatR;
using AutoMapper;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

namespace UserProfileService.Application.UseCases.Dish
{
    public class GetDishByIdHandler : IRequestHandler<GetDishByIdQuery, Domain.Entities.Dish>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDishByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Dish> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _unitOfWork.DishRepository.GetByIdAsync(request.Id);
            if (dish == null)
                throw new NotFoundException($"Dish not found.");

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(dish.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            return dish;
        }
    }
}
