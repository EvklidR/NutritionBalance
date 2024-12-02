using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using UserProfileService.Application.Models;
using System.Collections.Generic;
using UserProfileService.Application.Interfaces;

namespace UserProfileService.Infrastructure.Services
{
    public class SearchProductService : ISearchProductService
    {
        private const string ApiUrlSearch = "https://world.openfoodfacts.org/cgi/search.pl?search_terms={0}&json=true&lang=en";

        // Метод для поиска продуктов по названию
        public async Task<List<ProductResponse>?> GetProductsByName(string productName)
        {
            using (HttpClient client = new HttpClient())
            {
                // Формируем URL для поиска по названию с указанием языка
                string url = string.Format(ApiUrlSearch, productName);
                var response = await client.GetStringAsync(url);
                var searchResults = JsonConvert.DeserializeObject<SearchResponse>(response);

                List<ProductResponse> products = new List<ProductResponse>();

                if (searchResults != null && searchResults.Products != null)
                {
                    foreach (var product in searchResults.Products)
                    {
                        // Преобразуем данные в объект ProductResponse
                        products.Add(new ProductResponse(
                            product.Name,  // Оставляем название как есть (на английском)
                            product.Nutrition?.Calories ?? 0,
                            product.Nutrition?.Protein ?? 0,
                            product.Nutrition?.Fat ?? 0,
                            product.Nutrition?.Carbs ?? 0
                        ));
                    }
                }

                return products;
            }
        }
    }
}
