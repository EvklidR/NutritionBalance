using System.Threading.Tasks;
using UserProfileService.Domain.Interfaces.Repositories;
using UserProfileService.Infrastructure.MSSQL;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IDishRepository? _dishRepository;
        private IIngredientRepository? _ingredientRepository;
        private IProfileRepository? _profileRepository;
        private IDayResultRepository? _dayResultRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IDishRepository DishRepository
            => _dishRepository ??= new DishRepository(_context);

        public IIngredientRepository IngredientRepository
            => _ingredientRepository ??= new IngredientRepository(_context);

        public IProfileRepository ProfileRepository
            => _profileRepository ??= new ProfileRepository(_context);

        public IDayResultRepository DayResultRepository
            => _dayResultRepository ??= new DayResultRepository(_context);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
