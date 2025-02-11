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
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<JsonElement>(content);
            return new WeatherResponse
            {
                City = city,
                Temperature = data.GetProperty("main").GetProperty("temp").GetDouble(),
                MinTemperature = data.GetProperty("main").GetProperty("temp_min").GetDouble(),
                MaxTemperature = data.GetProperty("main").GetProperty("temp_max").GetDouble(),
                Precipitation = data.TryGetProperty("rain", out var rain) ? rain.GetProperty("1h").GetDouble() : 0,
                Description = data.GetProperty("weather")[0].GetProperty("description").GetString() ?? string.Empty
            };
        }
    }
}
