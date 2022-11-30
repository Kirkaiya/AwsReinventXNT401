using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text;

namespace aspnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JsonSerializeController : ControllerBase
    {
        // GET: api/<simulateasync>
        [HttpGet]
        public async Task<IEnumerable<string>> Get(int count = 100)
        {
            List<WeatherForecast> forecasts;
            List<string> jsons = new List<string>();

            using (var context = new WeatherContext())
            {
                forecasts = context.WeatherForecasts.Take(count).ToList();
            }
            await Task.Delay(5);
            Parallel.ForEach(forecasts, x => jsons.Add(JsonSerializer.Serialize(x)));

            return jsons;
        }
    }
}
