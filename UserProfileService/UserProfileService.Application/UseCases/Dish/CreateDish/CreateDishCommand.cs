using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Dish
{
    public class CreateDishCommand : IRequest<Domain.Entities.Dish>
    {
        public CreateDishDTO Dish { get; set; }

        public CreateDishCommand(CreateDishDTO dish)
        {
            Dish = dish;
        }
    }
}
