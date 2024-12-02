using MediatR;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public record GetIngredientsQuery(int ProfileId, int userId) : IRequest<IEnumerable<Domain.Entities.Ingredient>?>;
}
