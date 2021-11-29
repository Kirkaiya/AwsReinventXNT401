using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BenchmarkWebAPI.Weather;

namespace BenchmarkWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        // GET: /weatherforecast/5
        [HttpGet("{count?}")]
        public IEnumerable<WeatherForecast> Get(int count = 100)
        {
            _logger.LogInformation("GET WeatherForecast invoked!");
            
            return _weatherForecastService.Get(count);
        }
    }
}
