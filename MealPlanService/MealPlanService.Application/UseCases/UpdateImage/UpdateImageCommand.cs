using MediatR;
using Microsoft.AspNetCore.Http;

namespace MealPlanService.Application.UseCases
{
    public record UpdateImageCommand(IFormFile file, int mealPlanId, int userId) : IRequest;
}
