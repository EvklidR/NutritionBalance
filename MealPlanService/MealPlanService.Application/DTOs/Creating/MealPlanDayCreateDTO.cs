namespace MealPlanService.Application.DTOs
{
    public class MealPlanDayCreateDTO
    {
        public int NumberOfDay { get; set; }
        public double CaloriePercentage { get; set; }

        public List<NutrientOfDayCreateDTO> NutrientsOfDay { get; set; } = new List<NutrientOfDayCreateDTO>();
    }
}