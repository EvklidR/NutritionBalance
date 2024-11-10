using MealPlanService.Domain.Entities;

namespace MealPlanService.Domain.Interfaces
{
    public interface IMealPlanDayRepository
    {
        Task<MealPlanDay> GetByIdAsync(int id);
        Task<IEnumerable<MealPlanDay>> GetAllByMealPlanIdAsync(int mealPlanId);
        void Add(MealPlanDay mealPlanDay);
        void Update(MealPlanDay mealPlanDay);
        void Delete(int id);
    }
}
