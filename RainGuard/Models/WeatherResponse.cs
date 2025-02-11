namespace RainGuard.Models
{
    public class WeatherResponse
    {
        public string City { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double Precipitation { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
