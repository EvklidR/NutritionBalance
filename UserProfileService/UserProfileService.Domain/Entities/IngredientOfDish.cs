namespace UserProfileService.Domain.Entities
{
    public class IngredientOfDish
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }
        public double Weight { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
