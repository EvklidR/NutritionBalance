namespace MealPlanService.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> CheckUserByIdAsync(int userId);
    }
}
