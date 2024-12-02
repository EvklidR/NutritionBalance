namespace UserProfileService.Application.Models
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }

        public ProductResponse(string name, double calories, double proteins, double fats, double carbohydrates)
        {
            Name = name;
            Calories = calories;
            Proteins = proteins;
            Fats = fats;
            Carbohydrates = carbohydrates;
        }
    }
}
