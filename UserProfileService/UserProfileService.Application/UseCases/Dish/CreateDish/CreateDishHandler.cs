using MediatR;
using AutoMapper;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Application.Exceptions;

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
            var profile = await _unitOfWork.ProfileRepository.GetByIdAsync(request.Dish.ProfileId);

            if (profile == null)
                throw new NotFoundException("Profile not found");

            if (request.userId != profile!.UserId)
                throw new UnauthorizedException("Owner isn't valid");

            var dish = _mapper.Map<Domain.Entities.Dish>(request.Dish);
            
            var ingredients = new List<Domain.Entities.Ingredient>();

            double cals = 0, prots = 0, fats = 0, carbs = 0, weight = 0;

            foreach (var ingredient in dish.Ingredients)
            {
                var ing = await _unitOfWork.IngredientRepository.GetByIdAsync(ingredient.IngredientId);

                if (ing == null)
                    throw new NotFoundException($"Ingredient with id {ingredient.IngredientId} not found");

                cals += ing!.Calories * ingredient.Weight;
                prots += ing.Proteins * ingredient.Weight;
                fats += ing.Fats * ingredient.Weight;
                carbs += ing.Carbohydrates * ingredient.Weight;
                weight += ingredient.Weight;
            }

            dish.Calories = cals / weight;
            dish.Fats = fats / weight;
            dish.Proteins = prots / weight;
            dish.Carbohydrates = carbs / weight;

            _unitOfWork.DishRepository.Add(dish);
            await _unitOfWork.SaveChangesAsync();

            return dish;
        }
    }
}
