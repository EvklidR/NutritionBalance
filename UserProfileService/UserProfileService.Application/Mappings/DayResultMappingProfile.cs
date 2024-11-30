using AutoMapper;
using UserProfileService.Application.DTOs;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Application.Mappings
{
    public class DayResultMappingProfile : AutoMapper.Profile
    {
        public DayResultMappingProfile()
        {
            CreateMap<UpdateDayResultDTO, DayResult>()
                .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
                .ForMember(dest => dest.Meals, opt => opt.MapFrom(src => src.Meals));

            CreateMap<CreateDayResultDTO, DayResult>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Meals, opt => opt.MapFrom(src => src.Meals));

            CreateMap<CreateOrUpdateMealDTO, Meal>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DayId, opt => opt.Ignore())
                .ForMember(dest => dest.Foods, opt => opt.MapFrom(src => src.Foods));

            CreateMap<CreateOrUpdateEatenFoodDTO, EatenFood>()
                .ForMember(dest => dest.MealId, opt => opt.Ignore())
                .ForMember(dest => dest.Food, opt => opt.Ignore());
        }
    }
}
