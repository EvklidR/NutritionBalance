using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Ingredient.DeleteIngredient
{
    public class DeleteIngredientHandler : IRequestHandler<DeleteIngredientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteIngredientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(request.IngredientId);

            if (ingredient == null)
                throw new NotFoundException("Ingredient not found");

            var dishes = await _unitOfWork.DishRepository.GetAllAsync(ingredient.ProfileId);

            var dishesContains = dishes?.Where(d => d.Ingredients.Any(i => i.IngredientId == ingredient.Id));

            if (dishesContains.Any())
                throw new BadRequestException("This ingredient there is in some dish");

            _unitOfWork.IngredientRepository.Delete(ingredient);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
