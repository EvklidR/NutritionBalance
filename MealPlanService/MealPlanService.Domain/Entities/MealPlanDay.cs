namespace MealPlanService.Domain.Entities
{
    public class MealPlanDay
    {
        public int Id { get; set; }
        public int MealPlanId { get; set; }
        public int NumberOfDay { get; set; }
        public double CaloriePercentage { get; set; }


        public MealPlan MealPlan { get; set; } = null!;
        public List<NutrientOfDay> NutrientOfDay { get; set; } = new List<NutrientOfDay>();
    }

}
