using MediatR;

namespace UserProfileService.Application.UseCases.Dish
{
    public class GetDishByIdQuery : IRequest<Domain.Entities.Dish>
    {
        public int Id { get; set; }
    }
}
