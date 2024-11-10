using UserProfileService.Domain.Enums;

namespace UserProfileService.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public float ActivityLevel { get; set; } // уровень активности (1.2 - малоподвижный, 1.375 - низкий, 1.55 - средний, 1.725 - высокий)
        public int? MealPlanId { get; set; }
        public DateOnly DateOfStartPlan { get; set; }

    }
}
