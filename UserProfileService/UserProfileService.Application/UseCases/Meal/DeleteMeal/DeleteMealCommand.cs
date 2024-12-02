using MediatR;

namespace UserProfileService.Application.UseCases.Meal
{
    public record DeleteMealCommand(int MealId, int DayId, int userId) : IRequest;
}
