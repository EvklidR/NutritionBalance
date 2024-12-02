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
            CreateMap<UpdateMealPlanDTO, MealPlan>();
            CreateMap<MealPlanDayDTO, MealPlanDay>();
            CreateMap<NutrientOfDayDTO, NutrientOfDay>();
            CreateMap<MealPlan, MealPlan>();
        }
    }
}
