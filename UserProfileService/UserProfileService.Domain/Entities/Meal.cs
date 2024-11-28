

namespace UserProfileService.Domain.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public string Name { get; set; }

        public List<EatenFood> Foods { get; set; } = new List<EatenFood>();
    }
}
