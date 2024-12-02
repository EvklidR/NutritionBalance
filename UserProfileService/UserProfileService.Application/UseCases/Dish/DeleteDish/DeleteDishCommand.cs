using MediatR;

namespace UserProfileService.Application.UseCases.Dish
{
    public record DeleteDishCommand(int Id, int userId) : IRequest;
}
