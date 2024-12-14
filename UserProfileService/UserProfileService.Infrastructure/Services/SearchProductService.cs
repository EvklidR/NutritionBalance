﻿using Newtonsoft.Json;
using UserProfileService.Application.Models;
using UserProfileService.Application.Interfaces;

namespace UserProfileService.Infrastructure.Services
{
    public class SearchProductService : ISearchProductService
    {
        private const string ApiUrlSearch = "https://world.openfoodfacts.org/cgi/search.pl?search_terms={0}&json=true&lang=en";

        public async Task<List<ProductResponse>?> GetProductsByName(string productName)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format(ApiUrlSearch, productName);
                var response = await client.GetStringAsync(url);
                var searchResults = JsonConvert.DeserializeObject<SearchResponse>(response);

                List<ProductResponse> products = new List<ProductResponse>();

                if (searchResults != null && searchResults.Products != null)
                {
                    foreach (var product in searchResults.Products)
                    {
                        products.Add(new ProductResponse(
                            product.Name,
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
