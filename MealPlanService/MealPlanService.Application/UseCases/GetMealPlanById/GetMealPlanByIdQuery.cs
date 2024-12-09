using MealPlanService.Domain.Entities;
using MediatR;

namespace MealPlanService.Application.UseCases
{
    public record GetMealPlanByIdQuery(int mealPlanId) : IRequest<MealPlan>;
}