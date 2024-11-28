using UserProfileService.Domain.Entities;

namespace UserProfileService.Domain.Interfaces.Repositories
{
    public interface IDayResultRepository : IBaseRepository<DayResult>
    {
        Task<IEnumerable<DayResult>?> GetAllByPeriodAsync(int ProfileId, DateOnly dateStart, DateOnly dateEnd);
        Task<DayResult?> GetByDateAsync(int ProfileId, DateOnly date);
    }
}