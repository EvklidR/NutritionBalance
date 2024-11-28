using Microsoft.EntityFrameworkCore;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Interfaces.Repositories;
using UserProfileService.Infrastructure.MSSQL;

namespace UserProfileService.Infrastructure.Repositories
{
    public class DishRepository : BaseRepository<Dish>, IDishRepository
    {
        public DishRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Dish?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(d => d.Ingredients)
                .ThenInclude(iod => iod.Ingredient)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
