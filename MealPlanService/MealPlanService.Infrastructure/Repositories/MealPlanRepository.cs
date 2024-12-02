using MealPlanService.Infrastructure.MSSQL;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Enums;
using MealPlanService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MealPlanService.Infrastructure.Repositories
{
    public class MealPlanRepository : IMealPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public MealPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MealPlan?> GetByIdAsync(int id)
        {
            return await _context.MealPlans
                .Include(mp => mp.Days)
                .ThenInclude(d => d.NutrientsOfDay)
                .FirstOrDefaultAsync(mp => mp.Id == id);
        }
        public async Task<IEnumerable<MealPlan>?> GetByOwnerAsync(int id)
        {
            return await _context.MealPlans
                .Where(mp => mp.OwnerId == id)
                .Include(mp => mp.Days)
                .ThenInclude(d => d.NutrientsOfDay)
                .ToListAsync();
        }

        public async Task<IEnumerable<MealPlan>?> GetAllAsync()
        {
            return await _context.MealPlans.ToListAsync();
        }

        public async Task<IEnumerable<MealPlan>?> GetByTypeAsync(MealPlanType type, int pageNumber, int pageSize)
        {
            return await _context.MealPlans
                .Where(mp => mp.Type == type)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Add(MealPlan mealPlan)
        {
            _context.MealPlans.Add(mealPlan);
        }

        public void Update(MealPlan mealPlan)
        {
            _context.MealPlans.Update(mealPlan);
        }

        public void Delete(MealPlan mealPlan)
        {
            _context.MealPlans.Remove(mealPlan);
        }
    }
}
