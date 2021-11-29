using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using BenchmarkWebAPI.Weather;
using BenchmarkWebAPI.Hosting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BenchmarkWebAPI.Lambda {
    public class WeatherFunction : LambdaFunction {

        public WeatherFunction() {}
        
        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("Get Request\n");

            var result = new Dictionary<string, object> {
                { "hostInfo", new HostInfoService().GetHostInfo() },
                { "forecast", ServiceProvider.GetService<WeatherForecastService>().Get(100) }
            };

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(result),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };

            return response;
        }
    }
}