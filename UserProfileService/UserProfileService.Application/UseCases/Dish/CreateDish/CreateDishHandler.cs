using MediatR;
using AutoMapper;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Dish
{
    public class CreateDishHandler : IRequestHandler<CreateDishCommand, Domain.Entities.Dish>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDishHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Dish> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = _mapper.Map<Domain.Entities.Dish>(request.Dish);

            _unitOfWork.DishRepository.Add(dish);
            await _unitOfWork.SaveChangesAsync();

            return dish;
        }
    }
}
