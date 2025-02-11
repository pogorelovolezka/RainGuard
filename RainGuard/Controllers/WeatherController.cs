using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RainGuard.Services;

namespace RainGuard.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var weather = await _weatherService.GetWeatherAsync(city);
            return weather != null ? Ok(weather) : NotFound();
        }
    }
}
