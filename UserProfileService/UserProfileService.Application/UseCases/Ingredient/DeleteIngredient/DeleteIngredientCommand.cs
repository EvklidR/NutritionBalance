using MediatR;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public record DeleteIngredientCommand(int IngredientId, int userId) : IRequest;
}
