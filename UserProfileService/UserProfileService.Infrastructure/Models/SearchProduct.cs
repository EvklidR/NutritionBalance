using Newtonsoft.Json;

namespace UserProfileService.Infrastructure.Services
{
    public class SearchProduct
    {
        [JsonProperty("product_name")]
        public string Name { get; set; }

        [JsonProperty("nutriments")]
        public Nutrients Nutrition { get; set; }
    }
}
