namespace UserProfileService.Domain.Entities
{
    public class DayResult
    {
        public int ProfileId { get; set; }
        public DateOnly Date { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? ActivityLevel { get; set; }
        public int GlassesOfWater { get; set; }


        public List<EatenFood> EatenFood { get; set; } = new List<EatenFood>();
    }
}
