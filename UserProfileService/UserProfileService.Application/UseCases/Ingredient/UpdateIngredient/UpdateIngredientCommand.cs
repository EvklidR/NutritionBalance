using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Ingredient
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
