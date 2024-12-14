﻿namespace UserProfileService.Application.DTOs
{
    public class CreateDayResultDTO
    {
        public int ProfileId { get; set; }
        public DateOnly Date { get; set; }
        public double? Weight { get; set; }
        public int GlassesOfWater { get; set; } = 0;
    }
}
