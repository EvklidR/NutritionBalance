using UserProfileService.Domain.Entities;

namespace UserProfileService.Domain.Entities
{
    public class Dish : Food
    {
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double WeightOfPortion { get; set; }

        public List<IngredientOfDish> Ingredients { get; set; } = new List<IngredientOfDish>();
    }
}
