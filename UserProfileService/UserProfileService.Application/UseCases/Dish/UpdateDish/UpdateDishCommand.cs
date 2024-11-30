using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Dish
{
    public class UpdateDishCommand : IRequest
    {
        public UpdateDishDTO Dish { get; set; }

        public UpdateDishCommand(UpdateDishDTO dish)
        {
            Dish = dish;
        }
    }
}
