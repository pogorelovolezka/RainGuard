using RainGuard.Models;
using System.Text.Json;

namespace RainGuard.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _apiKey;
        
        public WeatherService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["OpenWeatherMap:ApiKey"] ?? throw new ArgumentNullException("API key is missing!");
        }

        public async Task<WeatherResponse?> GetWeatherAsync(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<JsonElement>(content);
            if (!data.TryGetProperty("list", out var forecastList)) return null;

            var firstForecast = forecastList.EnumerateArray().FirstOrDefault();
            if (firstForecast.ValueKind == JsonValueKind.Undefined) return null;

            return new WeatherResponse
            {
                City = city,
                Temperature = firstForecast.GetProperty("main").GetProperty("temp").GetDouble(),
                MinTemperature = firstForecast.GetProperty("main").GetProperty("temp_min").GetDouble(),
                MaxTemperature = firstForecast.GetProperty("main").GetProperty("temp_max").GetDouble(),
                Precipitation = firstForecast.TryGetProperty("rain", out var rain)
                    ? (rain.TryGetProperty("3h", out var r3) ? r3.GetDouble() : 0)
                    : 0,
                Description = firstForecast.GetProperty("weather")[0].GetProperty("description").GetString() ?? string.Empty
            };
        }

    }
}
