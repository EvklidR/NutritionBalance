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
        public ActivityLevel ActivityLevel { get; set; }
        public int DesiredGlassesOfWater { get; set; }
    }
}
