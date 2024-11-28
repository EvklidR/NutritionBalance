namespace UserProfileService.Application.DTOs
{
    public class CreateIngredientDTO
    {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
    }
}
