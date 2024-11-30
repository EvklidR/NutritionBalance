using MediatR;

namespace UserProfileService.Application.UseCases.Dish
{
    public class GetAllDishesQuery : IRequest<IEnumerable<Domain.Entities.Dish>?>
    {
        public int ProfileId { get; set; }
    }
}
