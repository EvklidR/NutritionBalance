﻿namespace UserProfileService.Domain.Entities
{
    public class DayResult
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public DateOnly Date { get; set; }
        public double? Weight { get; set; }
        public int GlassesOfWater { get; set; } = 0;


        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}
