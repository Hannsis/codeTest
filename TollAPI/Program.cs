using TollAPI.Controllers;
using TollAPI.Models;
using TollAPI.Services; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<TollCalculator>();
builder.Services.AddScoped<VehicleFactory>();

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>(); 

// if i reference hte interface i must reference the concrete type inside constructor( which is being used ) the DI cointainer
// it will be injected properly

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
