using MealPlanService.Domain.Enums;

namespace MealPlanService.Application.DTOs
{
    public class NutrientOfDayDTO
    {
        public NutrientType NutrientType { get; set; }
        public CalculationType CalculationType { get; set; }
        public double? Value { get; set; }
    }
}