namespace UserProfileService.Application.DTOs
{
    public class UpdateDishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int AmountOfPortions { get; set; }

        public List<IngredientOfDishDTO> Ingredients { get; set; } = new List<IngredientOfDishDTO>();
    }
}
