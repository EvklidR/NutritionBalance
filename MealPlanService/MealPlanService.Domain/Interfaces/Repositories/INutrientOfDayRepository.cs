using MealPlanService.Domain.Entities;

namespace MealPlanService.Domain.Interfaces
{
    public interface INutrientOfDayRepository
    {
        Task<NutrientOfDay> GetByIdAsync(int id);
        Task<IEnumerable<NutrientOfDay>> GetAllByMealPlanDayIdAsync(int mealPlanDayId);
        void Add(NutrientOfDay nutrientOfDay);
        void Update(NutrientOfDay nutrientOfDay);
        void Delete(int id);
    }
}
