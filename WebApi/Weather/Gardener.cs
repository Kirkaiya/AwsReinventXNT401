namespace BenchmarkWebAPI.Weather
{
    public static class Gardener
    {
        private static readonly string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        private static readonly IEnumerable<WeatherForecast> Forecasts = GetWeatherForecasts();

        public static void Seed(WeatherContext weatherContext)
        {
            weatherContext.Forecasts.AddRange(Forecasts);
            weatherContext.SaveChanges();
        }

        static IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            var weatherForecasts = new List<WeatherForecast>();
            var rand = new Random();

            for (int i = 0; i < 250; i++)
            {
                weatherForecasts.Add(new WeatherForecast { 
                    Id = Guid.NewGuid(), 
                    Date = DateTime.UtcNow, 
                    Summary = Summaries[rand.Next(Summaries.Length)], 
                    TemperatureC = rand.Next(-20, 55),
                });
            }

            return weatherForecasts;
        }
    }
}
