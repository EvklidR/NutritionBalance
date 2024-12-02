using Newtonsoft.Json;
using System.Collections.Generic;

namespace UserProfileService.Infrastructure.Services
{
    // Модель для десериализации ответа от поиска
    public class SearchResponse
    {
        [JsonProperty("products")]
        public List<SearchProduct> Products { get; set; }
    }
}
