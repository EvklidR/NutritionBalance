using UserProfileService.Application.DTOs;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Application.Mappings
{
    public class ProfileMappingProfile : AutoMapper.Profile
    {
        public ProfileMappingProfile()
        {
            CreateMap<CreateProfileDTO, Domain.Entities.Profile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityLevel, opt => opt.MapFrom(src => GetActivityMultiplier(src.ActivityLevel)));

            CreateMap<UpdateProfileDTO, Domain.Entities.Profile>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Birthday, opt => opt.Ignore())
                .ForMember(dest => dest.Gender, opt => opt.Ignore())
                .ForMember(dest => dest.MealPlanId, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfStartPlan, opt => opt.Ignore())
                .ForMember(dest => dest.DayResults, opt => opt.Ignore())

                .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) => src.Name ?? dest.Name))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom((src, dest) => src.Weight ?? dest.Weight))
                .ForMember(dest => dest.Height, opt => opt.MapFrom((src, dest) => src.Height ?? dest.Height))
                .ForMember(dest => dest.ActivityLevel, opt => opt.MapFrom((src, dest) => src.ActivityLevel.HasValue
                    ? GetActivityMultiplier(src.ActivityLevel)
                    : dest.ActivityLevel))
                .ForMember(dest => dest.DesiredGlassesOfWater, opt => opt.MapFrom((src, dest) => src.DesiredGlassesOfWater ?? dest.DesiredGlassesOfWater));
        }

        public double GetActivityMultiplier(ActivityLevel? activityLevel)
        {
            return activityLevel switch
            {
                ActivityLevel.sedentary => 1.2,
                ActivityLevel.low => 1.375,
                ActivityLevel.medium => 1.55,
                ActivityLevel.high => 1.725,
                ActivityLevel.veryHigh => 1.9,
                _ => 1.2, // Default multiplier in case the value is not valid
            };
        }
    }
}
