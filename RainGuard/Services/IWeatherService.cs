using RainGuard.Models;

namespace RainGuard.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse?> GetWeatherAsync(string city);
    }
}
