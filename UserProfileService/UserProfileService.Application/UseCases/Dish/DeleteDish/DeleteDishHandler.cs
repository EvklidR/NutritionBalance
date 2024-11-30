using MediatR;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Dish
{
    public class DeleteDishHandler : IRequestHandler<DeleteDishCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDishHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _unitOfWork.DishRepository.GetByIdAsync(request.Id);
            if (dish == null)
                throw new KeyNotFoundException($"Dish with ID {request.Id} not found.");

            _unitOfWork.DishRepository.Delete(dish);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
