using MealPlanService.Domain.Entities;
using MealPlanService.Domain.Enums;

namespace MealPlanService.Application.DTOs
{ 
    public class UpdateMealPlanDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MealPlanType Type { get; set; }

        public List<MealPlanDayDTO> Days { get; set; } = new List<MealPlanDayDTO>();
    }
}
