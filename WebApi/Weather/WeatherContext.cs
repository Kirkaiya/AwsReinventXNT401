using Microsoft.EntityFrameworkCore;

namespace BenchmarkWebAPI.Weather
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        public DbSet<WeatherForecast> Forecasts { get; set; }
    }
}
