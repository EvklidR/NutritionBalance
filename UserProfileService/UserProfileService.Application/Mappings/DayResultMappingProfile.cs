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
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityLevel, opt => opt.MapFrom(src => GetActivityMultiplier(src.ActivityLevel)));


            CreateMap<CreateDayResultDTO, DayResult>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityLevel, opt => opt.MapFrom(src => GetActivityMultiplier(src.ActivityLevel)));


            CreateMap<UpdateMealDTO, Meal>()
                .ForMember(dest => dest.Foods, opt => opt.MapFrom(src => src.Foods));

            CreateMap<CreateMealDTO, Meal>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DayId, opt => opt.Ignore())
                .ForMember(dest => dest.Foods, opt => opt.MapFrom(src => src.Foods));

            CreateMap<CreateOrUpdateEatenFoodDTO, EatenFood>()
                .ForMember(dest => dest.MealId, opt => opt.Ignore())
                .ForMember(dest => dest.Food, opt => opt.Ignore());
        }
        public double? GetActivityMultiplier(ActivityLevel? activityLevel)
        {
            return activityLevel switch
            {
                ActivityLevel.sedentary => 1.2,
                ActivityLevel.low => 1.375,
                ActivityLevel.medium => 1.55,
                ActivityLevel.high => 1.725,
                ActivityLevel.veryHigh => 1.9,
                null => null
            };
        }

    }
}
