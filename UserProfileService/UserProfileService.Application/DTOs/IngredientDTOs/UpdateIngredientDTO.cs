namespace UserProfileService.Application.DTOs
{
    public class UpdateIngredientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
    }
}
