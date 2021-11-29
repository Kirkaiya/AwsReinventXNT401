namespace BenchmarkWebAPI.Lambda {
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using BenchmarkWebAPI.Weather;

    public abstract class LambdaFunction {

        protected LambdaFunction(IServiceProvider serviceProvider = null)
        {
            ServiceProvider = serviceProvider ?? GetServiceCollection();

            ServiceProvider.GetService<ILogger<LambdaFunction>>()
                           ?.LogDebug( "Environment Variables {@EnvironmentVariables}", Environment.GetEnvironmentVariables());
        }

        protected static IServiceProvider ServiceProvider { get; private set; }

        [ExcludeFromCodeCoverage]
        private IServiceProvider GetServiceCollection() {
            if (ServiceProvider != null)
                return ServiceProvider;

            var host = Host.CreateDefaultBuilder()
                           .ConfigureServices(ConfigureServices)
                           .Build();

            Gardener.Seed(host.Services.GetService<WeatherContext>());

            return host.Services;
        }

        protected virtual void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services) {
            services.AddDbContext<WeatherContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase("forecasts"), ServiceLifetime.Transient);
            services.AddTransient<WeatherForecastService>();
        }
    }
}