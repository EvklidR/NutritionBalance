using MediatR;

namespace UserProfileService.Application.UseCases.Dish
{
    public record GetDishByIdQuery(int Id, int userId) : IRequest<Domain.Entities.Dish>;
}
