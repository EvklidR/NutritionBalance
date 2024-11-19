using MediatR;
using MealPlanService.Domain.Entities;

namespace MealPlanService.Application.UseCases
{
    public record UpdateMealPlanCommand(MealPlan MealPlan) : IRequest;
}
