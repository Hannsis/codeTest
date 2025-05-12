using Microsoft.AspNetCore.Mvc;
using TollAPI.Models;
using TollAPI.Services;


namespace TollAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TollEventsController : ControllerBase
{
    // dependancies for all the http requests
    public TollCalculator _calculator; // = new();
    
    public TollEventsController() 
    {
        _calculator = new TollCalculator();
    }
       
    [HttpPost("getTollFee")] 
    public async Task<IActionResult> Record([FromBody]TollFeeRequest TollFeeRequest)
    {   
        // need to map VehicleType from sting to object
         Vehicle vehicle = TollFeeRequest.VehicleType switch
        {
            "Car" => new Car(),
            _ => throw new ArgumentException("Unsupported vehicle type.")
        };

        //int totalFee = _calculator.GetTollFee(TollFeeRequest.Vehicle, TollFeeRequest.Dates);
         int totalFee = _calculator.GetTollFee(vehicle, TollFeeRequest.Dates);
        return Ok("Toll fee calculated: " + totalFee);
    }

//sending POST: ASP.NET tries to convert the request body JSON to TollFeeRequest

}


    // på sikt addera en till endpoint som tar emot flera TollFeeRequests
    // try catch - GetTollFee
    // return badRquest