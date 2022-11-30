using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace aspnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;   
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(int count = 100)
        {
            List<WeatherForecast> forecasts;

            using (var context = new WeatherContext())
            {
                forecasts = context.WeatherForecasts.Take(count).ToList();
            }
            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                Parallel.ForEach(forecasts, x => x.EncryptedSummary = RSAEncrypt(ByteConverter.GetBytes(x.Summary), RSA.ExportParameters(false), false));

            return forecasts;
        }

        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This only needs to include the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding. OAEP padding is only available on Microsoft Windows XP or later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }

                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

    }
}
