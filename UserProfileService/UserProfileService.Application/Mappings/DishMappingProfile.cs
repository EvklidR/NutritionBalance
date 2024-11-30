using UserProfileService.Application.DTOs;
using UserProfileService.Domain.Entities;

namespace UserProfileService.Application.Mapping
{
    public class DishMappingProfile : AutoMapper.Profile
    {
        public DishMappingProfile()
        {
            CreateMap<CreateDishDTO, Dish>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.WeightOfPortion, opt => opt.MapFrom(src =>
                    src.Ingredients.Sum(i => i.Weight) / src.AmountOfPortions));

            CreateMap<UpdateDishDTO, Dish>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.WeightOfPortion, opt => opt.MapFrom(src =>
                    src.Ingredients.Sum(i => i.Weight) / src.AmountOfPortions));

            CreateMap<IngredientOfDishDTO, IngredientOfDish>();
        }
    }
}
