using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Dish
{
    public class GetAllDishesHandler : IRequestHandler<GetAllDishesQuery, IEnumerable<Domain.Entities.Dish>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDishesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Entities.Dish>?> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var dishes = await _unitOfWork.DishRepository.GetAllAsync(request.ProfileId);
            return dishes;
        }
    }
}
