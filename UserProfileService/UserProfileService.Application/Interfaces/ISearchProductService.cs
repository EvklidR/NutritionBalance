using UserProfileService.Application.Models;

namespace UserProfileService.Application.Interfaces
{
    public interface ISearchProductService
    {
        Task<List<ProductResponse>?> GetProductsByName(string name);
    }
}
