using MediatR;

namespace UserProfileService.Application.UseCases.Dish
{
    public record GetAllDishesQuery(int ProfileId, int userId) : IRequest<IEnumerable<Domain.Entities.Dish>?>;
}
