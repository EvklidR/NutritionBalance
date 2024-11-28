using AutoMapper;
using UserProfileService.Application.DTOs;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Application.Mappings
{
    public class IngredientMappingProfile : AutoMapper.Profile
    {
        public IngredientMappingProfile() 
        {
            CreateMap<CreateIngredientDTO, Ingredient>()
                .ForMember(dest =>  dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Calories, opt => opt.MapFrom(src => src.Carbohydrates * 4 + src.Proteins * 4 + src.Fats * 9));

            CreateMap<UpdateIngredientDTO, Ingredient>()
                .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
                .ForMember(dest => dest.Calories, opt => opt.MapFrom(src => src.Carbohydrates * 4 + src.Proteins * 4 + src.Fats * 9));

        }
    }
}
