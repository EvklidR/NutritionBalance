using UserProfileService.Domain.Enums;

namespace UserProfileService.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateOnly Birthday { get; set; }
        public Gender Gender { get; set; }
        public double ActivityLevel { get; set; } // уровень активности (1.2 - малоподвижный, 1.375 - низкий, 1.55 - средний, 1.725 - высокий)
        public int DesiredGlassesOfWater { get; set; }
        public int? MealPlanId { get; set; }
        public DateOnly DateOfStartPlan { get; set; }

    }
}
