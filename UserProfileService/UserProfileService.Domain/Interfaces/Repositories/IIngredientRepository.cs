using UserProfileService.Domain.Entities;

namespace UserProfileService.Domain.Interfaces.Repositories
{
    public interface IIngredientRepository
    {
        Task<Ingredient?> GetByIdAsync(int id);
        Task<IEnumerable<Ingredient>?> GetAllAsync(int profileId);
        void Add(Ingredient ingredient);
        void Update(Ingredient ingredient);
        void Delete(int id);
    }
}
