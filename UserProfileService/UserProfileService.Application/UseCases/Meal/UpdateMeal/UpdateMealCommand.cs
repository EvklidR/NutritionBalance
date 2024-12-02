using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Meal
{
    public record UpdateMealCommand(UpdateMealDTO UpdateMealDTO, int userId) : IRequest;
}
