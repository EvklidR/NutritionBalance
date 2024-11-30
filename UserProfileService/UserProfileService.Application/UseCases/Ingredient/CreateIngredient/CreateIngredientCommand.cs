using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class CreateIngredientCommand : IRequest<Domain.Entities.Ingredient>
    {
        public CreateIngredientDTO IngredientDTO { get; set; }

        public CreateIngredientCommand(CreateIngredientDTO ingredientDTO)
        {
            IngredientDTO = ingredientDTO;
        }
    }
}