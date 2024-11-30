namespace UserProfileService.Application.DTOs
{
    public class UpdateDayResultDTO
    {
        public int Id { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? ActivityLevel { get; set; }
        public int GlassesOfWater { get; set; }

        public List<CreateOrUpdateMealDTO> Meals { get; set; } = new List<CreateOrUpdateMealDTO>();
    }
}
