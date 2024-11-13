using UserProfileService.Domain.Entities;

namespace UserProfileService.Domain.Interfaces.Repositories
{
    public interface IDishRepository
    {
        Task<Dish?> GetByIdAsync(int id);
        Task<IEnumerable<Dish>?> GetAllAsync(int profileId);
        void Add(Dish dish);
        void Update(Dish dish);
        void Delete(int id);
    }
}
