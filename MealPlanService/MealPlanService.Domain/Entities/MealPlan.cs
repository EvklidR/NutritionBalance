using MealPlanService.Domain.Enums;

namespace MealPlanService.Domain.Entities
{
    public class MealPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MealPlanType Type { get; set; }
        public string? ImageUrl { get; set; }

        public List<MealPlanDay> Days { get; set; } = new List<MealPlanDay>();
    }

}
