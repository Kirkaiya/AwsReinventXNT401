using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BenchmarkWebAPI.Weather;

namespace BenchmarkWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JsonSerializeController : ControllerBase
    {
        private readonly ILogger<JsonSerializeController> _logger;
        private readonly WeatherContext _weatherContext;

        public JsonSerializeController(ILogger<JsonSerializeController> logger, WeatherContext weatherContext)
        {
            _logger = logger;
            _weatherContext = weatherContext;
        }

        // GET: /jsonserialize/5
        [HttpGet("{count?}")]
        public async Task<IEnumerable<string>> Get(int count = 100)
        {
            _logger.LogInformation("GET JsonSerialze invoked!");
            List<WeatherForecast> forecasts;
            List<string> jsons = new List<string>(count);

            using (var context = _weatherContext)
            {
                forecasts = context.Forecasts.Take(count).ToList();
            }

            await Task.Delay(5);
            Parallel.ForEach(forecasts, x => jsons.Add(JsonSerializer.Serialize(x)));

            return jsons;
        }
    }
}
