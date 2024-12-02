using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Dish
{
    public record CreateDishCommand(CreateDishDTO Dish, int userId) : IRequest<Domain.Entities.Dish>;
}
