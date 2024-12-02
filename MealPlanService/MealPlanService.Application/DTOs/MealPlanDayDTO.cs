namespace MealPlanService.Application.DTOs
{
    public class MealPlanDayDTO
    {
        public int NumberOfDay { get; set; }
        public double CaloriePercentage { get; set; }

        public List<NutrientOfDayDTO> NutrientsOfDay { get; set; } = new List<NutrientOfDayDTO>();
    }
}