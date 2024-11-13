using MealPlanService.Infrastructure.MSSQL;
using MealPlanService.Domain.Interfaces;

namespace MealPlanService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IMealPlanRepository _mealPlan;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IMealPlanRepository MealPlans => _mealPlan ??= new MealPlanRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
