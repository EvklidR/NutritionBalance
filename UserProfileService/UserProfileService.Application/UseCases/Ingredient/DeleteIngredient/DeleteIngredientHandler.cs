using MediatR;
using UserProfileService.Application.Exceptions;
using UserProfileService.Domain.Entities;
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

            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(ingredient.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var dishes = await _unitOfWork.DishRepository.GetAllAsync(ingredient.ProfileId);
            var dishesContains = dishes?.Where(d => d.Ingredients.Any(i => i.IngredientId == ingredient.Id));
            if (dishesContains.Any())
                throw new BadRequestException("This ingredient there is in some dish");

            var mealsDays = await _unitOfWork.DayResultRepository.GetAllAsync(ingredient.ProfileId);
            foreach (var day in mealsDays) 
            {
                foreach (var meal in day.Meals)
                { 
                    foreach (var food in meal.Foods)
                    {
                        if (food.FoodId == ingredient.Id)
                            throw new BadRequestException("This ingredient there is in some meals");
                    }
                }
            }

            _unitOfWork.IngredientRepository.Delete(ingredient);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
