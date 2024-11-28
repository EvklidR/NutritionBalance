namespace UserProfileService.Domain.Entities
{
    public class EatenFood
    {
        public int FoodId { get; set; }
        public int MealId { get; set; }
        public double Weight { get; set; }

        public Food Food { get; set; }
    }
}
