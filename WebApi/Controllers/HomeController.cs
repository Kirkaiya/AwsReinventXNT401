using BenchmarkWebAPI.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BenchmarkWebAPI.Controllers
{
    [ApiController]
    public class JsonController : Controller
    {
        [HttpGet("/")]
        [Produces("application/json")]
        public HostInfo Json()
        {
            var hostInfoService = new HostInfoService();

            return hostInfoService.GetHostInfo();
        }
    }
}