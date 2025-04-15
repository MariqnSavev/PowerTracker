using System.Net.Http.Headers;
using System.Text.Json;
using PowerTracker.Models.Nutritionix;
namespace PowerTracker.Services
{

    public interface INutritionixService
    {
        Task<ParsedFood?> SearchFoodAsync(string query);
    }
    public class NutritionixService : INutritionixService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public NutritionixService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ParsedFood?> SearchFoodAsync(string query)
        {
            var appId = _configuration["Nutritionix:AppId"];
            var appKey = _configuration["Nutritionix:AppKey"];

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-app-id", appId);
            _httpClient.DefaultRequestHeaders.Add("x-app-key", appKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent($"{{\"query\":\"{query}\"}}", System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://trackapi.nutritionix.com/v2/natural/nutrients", content);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);
            var foodJson = document.RootElement.GetProperty("foods")[0];

            return new ParsedFood
            {
                FoodName = foodJson.GetProperty("food_name").GetString(),
                BrandName = foodJson.GetProperty("brand_name").GetString(),
                ServingQty = foodJson.GetProperty("serving_qty").GetDouble(),
                ServingUnit = foodJson.GetProperty("serving_unit").GetString(),
                ServingWeightGrams = foodJson.GetProperty("serving_weight_grams").GetDouble(),
                Calories = foodJson.GetProperty("nf_calories").GetDouble(),
                TotalFat = foodJson.GetProperty("nf_total_fat").GetDouble(),
                TotalCarbohydrate = foodJson.GetProperty("nf_total_carbohydrate").GetDouble(),
                Protein = foodJson.GetProperty("nf_protein").GetDouble(),
                Photo = foodJson.GetProperty("photo").GetProperty("thumb").GetString()
            };
        }
    }
}