namespace UserProfileService.Domain.Entities
{
    public abstract class Food
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }

        public Profile Profile { get; set; }
    }
}
