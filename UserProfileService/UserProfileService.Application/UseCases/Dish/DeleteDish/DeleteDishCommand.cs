using MediatR;

namespace UserProfileService.Application.UseCases.Dish
{
    public class DeleteDishCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteDishCommand(int id)
        {
            Id = id;
        }
    }
}
