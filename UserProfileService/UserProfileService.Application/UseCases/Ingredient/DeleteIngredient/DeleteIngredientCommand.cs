using MediatR;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class DeleteIngredientCommand : IRequest
    {
        public int IngredientId { get; set; }

        public DeleteIngredientCommand(int ingredientId)
        {
            IngredientId = ingredientId;
        }
    }
}
