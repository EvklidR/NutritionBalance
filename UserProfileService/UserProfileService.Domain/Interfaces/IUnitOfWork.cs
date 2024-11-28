using UserProfileService.Domain.Interfaces.Repositories;

namespace UserProfileService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IDishRepository DishRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IDayResultRepository DayResultRepository { get; }
        Task SaveChangesAsync();
    }
}
