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
        public double ActivityLevel { get; set; }
        public int DesiredGlassesOfWater { get; set; }
        public int? MealPlanId { get; set; }
        public DateOnly? DateOfStartPlan { get; set; }

        public List<DayResult> DayResults { get; set; } = new List<DayResult>();
    }
}
