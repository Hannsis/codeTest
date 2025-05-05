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
        // curl http://localhost:5251/api/WeatherForecast/example

    }
    
    // this definition will allow me to have multiple endpoints in the same controller that respond to the HTTP get verb
    [HttpGet("otherEndpoint")]
    public WeatherForecast GetOtherEndpoint()
    {
        var result = _weatherForecastService.Get().First();
        return result;
        // curl http://localhost:5251/api/WeatherForecast/otherEndpoint

    }

    [HttpPost]
    public string TjenaTjabbaHallu([FromBody]string name)
    {
        return $"hello {name}";
        // curl -X POST http://localhost:5251/api/WeatherForecast -H "Content-Type: application/json" -d "\"Hanna\""
    }

}
