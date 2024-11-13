using UserProfileService.Domain.Interfaces.Repositories;

namespace UserProfileService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IDishRepository DishRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        IIngredientOfDishRepository IngredientOfDishRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IDayResultRepository dayResultRepository { get; }
        Task SaveChangesAsync();
    }
}
