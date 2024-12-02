using MediatR;
using UserProfileService.Application.DTOs;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public record CreateIngredientCommand(CreateIngredientDTO IngredientDTO, int userId) : IRequest<Domain.Entities.Ingredient>;
}
