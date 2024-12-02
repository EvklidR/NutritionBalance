using MediatR;
using MealPlanService.Application.Models;

namespace MealPlanService.Application.UseCases
{
    public record GetKcalAndMacrosQuery(int mealPlanId, double bodyWeight, double daylyKcal, DateOnly startDate) 
        : IRequest<DailyNeedsResponse>;
}
