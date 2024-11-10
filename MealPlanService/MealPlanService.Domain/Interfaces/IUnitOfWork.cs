namespace MealPlanService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IMealPlanRepository MealPlanRepository { get; }
        IMealPlanDayRepository MealPlanDayRepository { get; }
        INutrientOfDayRepository NutrientOfDayRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
