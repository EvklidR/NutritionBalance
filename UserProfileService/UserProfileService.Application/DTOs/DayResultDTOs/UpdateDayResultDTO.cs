namespace UserProfileService.Application.DTOs
{
    public class UpdateDayResultDTO
    {
        public int Id { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public ActivityLevel? ActivityLevel { get; set; }
        public int GlassesOfWater { get; set; }
    }
}
