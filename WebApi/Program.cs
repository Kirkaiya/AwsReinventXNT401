using BenchmarkWebAPI;
using BenchmarkWebAPI.Weather;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<WeatherForecastService>();
builder.Services.AddDbContext<WeatherContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase("forecasts"));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "EC2BenchmarkingNet6", Version = "v1" });
});

var app = builder.Build();

Gardener.Seed(app.Services.GetService<WeatherContext>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EC2BenchmarkingNet6 v1"));
}

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
