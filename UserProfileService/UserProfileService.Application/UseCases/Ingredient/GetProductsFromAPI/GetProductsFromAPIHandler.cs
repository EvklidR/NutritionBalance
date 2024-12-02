using MediatR;
using UserProfileService.Application.Interfaces;
using UserProfileService.Application.Models;

namespace UserProfileService.Application.UseCases.Ingredient
{
    public class GetProductsFromAPIHandler : IRequestHandler<GetProductsFromAPIQuery, List<ProductResponse>?>
    {
        private readonly ISearchProductService _searchProductService;
        public GetProductsFromAPIHandler(ISearchProductService searchProductService)
        {
            _searchProductService = searchProductService;
        }

        public async Task<List<ProductResponse>?> Handle(GetProductsFromAPIQuery request, CancellationToken cancellationToken)
        {
            return await _searchProductService.GetProductsByName(request.Name);
        }
    }
}
