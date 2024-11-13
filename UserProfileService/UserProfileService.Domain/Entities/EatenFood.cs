namespace UserProfileService.Domain.Entities
{
    public class EatenFood
    {
        public int FoodId { get; set; }
        public int DayResultId { get; set; }
        public double Weight { get; set; }

        public Food Food { get; set; }
    }
}
