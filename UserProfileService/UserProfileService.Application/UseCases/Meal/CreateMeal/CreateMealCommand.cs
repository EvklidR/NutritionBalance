using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Meal
{
    public record CreateMealCommand(CreateMealDTO CreateMealDTO, int userId) : IRequest<Domain.Entities.Meal>;
}
