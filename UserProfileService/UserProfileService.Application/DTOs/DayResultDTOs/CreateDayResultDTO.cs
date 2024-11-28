namespace UserProfileService.Application.DTOs
{
    public class CreateDayResultDTO
    {
        public int ProfileId { get; set; }
        public DateOnly Date { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? ActivityLevel { get; set; }
        public int GlassesOfWater { get; set; } = 0;


        public List<CreateMealDTO>? Meals { get; set; } = new List<CreateMealDTO>();
    }
}
