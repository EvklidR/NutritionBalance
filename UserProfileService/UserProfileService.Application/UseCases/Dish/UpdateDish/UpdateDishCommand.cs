using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Dish
{
    public record UpdateDishCommand(UpdateDishDTO Dish, int userId) : IRequest;
}
