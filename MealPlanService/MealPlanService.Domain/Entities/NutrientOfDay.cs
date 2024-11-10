using MealPlanService.Domain.Enums;

namespace MealPlanService.Domain.Entities
{
    public class NutrientOfDay
    {
        public int Id { get; set; }
        public int MealPlanDayId { get; set; }
        public NutrientType NutrientType { get; set; }
        public CalculationType CalculationType { get; set; }
        public double? Value { get; set; }
    }
}
