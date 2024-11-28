using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Interfaces.Repositories;

namespace UserProfileService.Domain.Interfaces
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
    }
}
