using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Domain.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile> GetByIdAsync(int id);
        Task<IEnumerable<Profile>?> GetAllByUserIdAsync(int userId);
        Task<IEnumerable<Profile>> GetAllAsync();
        void Add(Profile profile);
        void Update(Profile profile);
        void Delete(int id);
        Task SaveAsync();
    }
}
