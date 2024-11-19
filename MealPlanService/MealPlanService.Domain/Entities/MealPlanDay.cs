namespace MealPlanService.Domain.Entities
{
    public class MealPlanDay
    {
        public int Id { get; set; }
        public int MealPlanId { get; set; }
        public int NumberOfDay { get; set; }
        public double CaloriePercentage { get; set; } = 1;

        public List<NutrientOfDay> NutrientsOfDay { get; set; } = new List<NutrientOfDay>();
    }

}
