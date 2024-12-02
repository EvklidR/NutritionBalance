using MediatR;
using MealPlanService.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace MealPlanService.Application.UseCases
{
    public record UpdateMealPlanCommand(UpdateMealPlanDTO MealPlan, int userId) : IRequest;
}
