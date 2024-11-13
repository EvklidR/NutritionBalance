using UserProfileService.Domain.Entities;

namespace UserProfileService.Domain.Interfaces.Repositories
{
    public interface IIngredientOfDishRepository
    {
        Task<IEnumerable<IngredientOfDish>> GetByDishIdAsync(int dishId);
        void Add(IngredientOfDish ingredientOfDish);
        void Delete(int dishId, int ingredientId);
        void Update(IngredientOfDish ingredientOfDish);
    }
}
