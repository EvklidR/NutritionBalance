using Microsoft.AspNetCore.Http;

namespace UserProfileService.Application.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
        void DeleteImage(string fileName);
        Task<byte[]> GetImageAsync(string fileName);
        Task<byte[]> GetCashedImageAsync(string fileName);

    }
}
