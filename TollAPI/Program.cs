using TollAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>(); 
// if i reference hte interface i must reference the concrete type inside constructor( which is being used ) the DI cointainer
// it will be injected properly
//chatGPT can tell you differences. 
// builder.Services.AddSingleton();
// builder.Services.AddTransient();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
