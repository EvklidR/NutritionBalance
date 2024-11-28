using MediatR;
using UserProfileService.Application.DTOs.IngredientDTOs;

namespace UserProfileService.Application.UseCases.Ingredient.UpdateIngredient
{
    public class UpdateIngredientCommand : IRequest
    {
        public UpdateIngredientDTO IngredientDTO { get; set; }

        public UpdateIngredientCommand(UpdateIngredientDTO ingredientDTO)
        {
            IngredientDTO = ingredientDTO;
        }
    }
}
