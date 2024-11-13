using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Enums;

namespace MealPlanService.Domain.Interfaces
{
    public interface IMealPlanRepository
    {
        Task<MealPlan?> GetByIdAsync(int id);
        Task<IEnumerable<MealPlan>?> GetAllAsync();
        Task<IEnumerable<MealPlan>?> GetByTypeAsync(MealPlanType type, int pageNumber, int pageSize);
        void Add(MealPlan mealPlan);
        void Update(MealPlan mealPlan);
        void Delete(int id);
    }
}
