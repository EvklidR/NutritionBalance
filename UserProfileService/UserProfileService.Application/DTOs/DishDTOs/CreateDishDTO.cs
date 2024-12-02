using Microsoft.AspNetCore.Http;

namespace UserProfileService.Application.DTOs
{
    public class CreateDishDTO
    {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int AmountOfPortions { get; set; }

        public List<IngredientOfDishDTO> Ingredients { get; set; } = new List<IngredientOfDishDTO>();
    }
}
