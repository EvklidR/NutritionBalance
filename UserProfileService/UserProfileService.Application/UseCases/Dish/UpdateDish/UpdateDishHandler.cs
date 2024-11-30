using MediatR;
using AutoMapper;
using UserProfileService.Domain.Interfaces;

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
                throw new KeyNotFoundException($"Dish with ID {request.Dish.Id} not found.");

            _mapper.Map(request.Dish, dish);
            _unitOfWork.DishRepository.Update(dish);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
