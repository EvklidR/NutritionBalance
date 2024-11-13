namespace MealPlanService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IMealPlanRepository MealPlans { get; }
        //IMealPlanDayRepository MealPlanDays { get; }
        //INutrientOfDayRepository NutrientsOfDay { get; }

        Task<int> SaveChangesAsync();
    }
}
