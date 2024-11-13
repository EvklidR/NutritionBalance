using UserProfileService.Domain.Entities;

namespace UserProfileService.Domain.Interfaces.Repositories
{
    public interface IDayResultRepository
    {
        Task<DayResult?> GetByIdAsync(int profileId, DateOnly date);
        Task<IEnumerable<DayResult>> GetByProfileIdAsync(int profileId);
        void Add(DayResult dayResult);
        void Update(DayResult dayResult);
        void Delete(int profileId, DateOnly date);
    }
}
