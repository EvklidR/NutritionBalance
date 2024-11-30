using MediatR;
using AutoMapper;
using UserProfileService.Domain.Interfaces.Repositories;

namespace UserProfileService.Application.UseCases.Dish
{
    public class GetAllDishesHandler : IRequestHandler<GetAllDishesQuery, IEnumerable<Domain.Entities.Dish>?>
    {
        private readonly IDishRepository _dishRepository;

        public GetAllDishesHandler(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Dish>?> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _dishRepository.GetAllAsync(request.ProfileId);
            return dishes;
        }
    }
}
