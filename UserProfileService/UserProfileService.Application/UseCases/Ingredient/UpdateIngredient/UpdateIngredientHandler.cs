using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Application.UseCases.Ingredient.UpdateIngredient
{
    public class UpdateIngredientHandler : IRequestHandler<UpdateIngredientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateIngredientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(request.IngredientDTO.Id);

            if (ingredient == null)
                throw new NotFoundException("Ingredient not found");
            


            _unitOfWork.IngredientRepository.Update(ingredient);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
