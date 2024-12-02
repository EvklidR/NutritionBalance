using MediatR;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Enums;

namespace MealPlanService.Application.UseCases
{
    public record GetMealPlansByTypeQuery(MealPlanType Type, int PageNumber, int PageSize) : IRequest<IEnumerable<MealPlan>>;
}
