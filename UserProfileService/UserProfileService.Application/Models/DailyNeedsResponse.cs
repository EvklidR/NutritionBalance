namespace UserProfileService.Application.Models
{
    public class DailyNeedsResponse
    {
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public DailyNeedsResponse() { }
        public DailyNeedsResponse(double calories, double proteins, double fats, double carbohydrates)
        {
            Calories = calories;
            Proteins = proteins;
            Fats = fats;
            Carbohydrates = carbohydrates;
        }
    }
}
