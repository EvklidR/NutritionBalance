using MealPlanService.Domain.Enums;

namespace MealPlanService.Application.DTOs
{
    public class MealPlanCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MealPlanType Type { get; set; }

        public List<MealPlanDayCreateDTO> Days { get; set; } = new List<MealPlanDayCreateDTO>();
    }
}
