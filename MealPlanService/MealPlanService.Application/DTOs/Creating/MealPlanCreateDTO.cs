using MealPlanService.Domain.Enums;
using Microsoft.AspNetCore.Http;


namespace MealPlanService.Application.DTOs
{
    public class MealPlanCreateDTO
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MealPlanType Type { get; set; }
        public List<MealPlanDayDTO> Days { get; set; } = new List<MealPlanDayDTO>();
    }
}
