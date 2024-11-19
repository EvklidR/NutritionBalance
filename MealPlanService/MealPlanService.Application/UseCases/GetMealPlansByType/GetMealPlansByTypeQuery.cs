using MediatR;
using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Enums;

public record GetMealPlansByTypeQuery(MealPlanType Type, int PageNumber, int PageSize) : IRequest<IEnumerable<MealPlan>>;
