using MediatR;
using MealPlanService.Application.DTOs;
using MealPlanService.Domain.Entities;

namespace MealPlanService.Application.UseCases
{
    public record CreateMealPlanCommand(MealPlanCreateDTO MealPlanDto) : IRequest<MealPlan>;
}
