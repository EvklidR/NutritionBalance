using MediatR;
using Microsoft.AspNetCore.Http;

namespace UserProfileService.Application.UseCases.Dish
{
    public record UpdateImageCommand(IFormFile file, int dishId, int userId) : IRequest;
}
