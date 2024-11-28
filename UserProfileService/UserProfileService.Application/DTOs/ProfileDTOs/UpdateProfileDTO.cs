using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Enums;

namespace UserProfileService.Application.DTOs
{
    public class UpdateProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public ActivityLevel ActivityLevel { get; set; } // уровень активности (1.2 - малоподвижный, 1.375 - низкий, 1.55 - средний, 1.725 - высокий, 1.9 - )
        public int DesiredGlassesOfWater { get; set; }
    }
}
