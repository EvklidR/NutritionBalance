using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public record UpdateIngredientCommand(UpdateIngredientDTO IngredientDTO, int userId) : IRequest;
}
