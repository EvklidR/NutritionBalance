using AutoMapper;
using MealPlanService.Application.DTOs;
using MealPlanService.Domain.Entities;

namespace MealPlanService.Application.Mappings
{
    public class MappingMealPlanProfile : Profile
    {
        public MappingMealPlanProfile()
        {
            CreateMap<MealPlanCreateDTO, MealPlan>();
            CreateMap<MealPlanDayCreateDTO, MealPlanDay>();
            CreateMap<NutrientOfDayCreateDTO, NutrientOfDay>();
            CreateMap<MealPlan, MealPlan>();
        }
    }
}
