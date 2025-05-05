    // the weather forecast class is a dependancy inside the controller class, no change to the logic, just refactoring the logic
    // instead of instanciating a new service, they are highly coupled, inject the refernce into the constructor

using Microsoft.AspNetCore.Mvc;
using System;

namespace TollAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WeatherForecastController : ControllerBase
{
    // dependancies
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _weatherForecastService; // = new();

    // Constructors - inject 
    public WeatherForecastController(ILogger<WeatherForecastController> logger, 
    IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    [Route("example")]
    public IEnumerable<WeatherForecast> Get()
    {
        var result = _weatherForecastService.Get();
        return result;
    }

}
