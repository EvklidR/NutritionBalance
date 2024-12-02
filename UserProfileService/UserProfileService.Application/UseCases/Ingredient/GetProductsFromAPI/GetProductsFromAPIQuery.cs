using MediatR;
using UserProfileService.Application.Models;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public record GetProductsFromAPIQuery(string Name) : IRequest<List<ProductResponse>?>;
}
