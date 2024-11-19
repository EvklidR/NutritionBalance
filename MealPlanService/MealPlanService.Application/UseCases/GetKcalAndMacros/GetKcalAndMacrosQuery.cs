using MediatR;
using MealPlanService.Domain.Enums;

namespace MealPlanService.Application.UseCases
{
    public record GetKcalAndMacrosQuery(int mealPlanId, double bodyWeight, double daylyKcal, DateOnly startDate) : IRequest<(double Calories, double Protein, double Fat, double Carbs)>;
}
