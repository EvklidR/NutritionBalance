using MealPlanService.Domain.Entities;
using MediatR;

namespace MealPlanService.Application.UseCases
{
    public record GetMealPlansByOwnerQuery(int ownerId) : IRequest<IEnumerable<MealPlan>?>;
}
