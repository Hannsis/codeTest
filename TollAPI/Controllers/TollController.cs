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

    try
    {
        if (TollFeeRequest == null || TollFeeRequest.VehicleType == null || TollFeeRequest.Dates == null)
            return BadRequest("Request, vehicleType, and dates are required.");

        // need to map VehicleType from sting to object
         Vehicle vehicle = TollFeeRequest.VehicleType switch
        {
            "Car" => new Car(),
            _ => throw new ArgumentException("Unsupported vehicle type.")
        };

         int totalFee = _calculator.GetTollFee(vehicle, TollFeeRequest.Dates);
        return Ok("Toll fee calculated: " + totalFee);
    }

    catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            // Log error if needed
            return StatusCode(500, "Something went wrong while calculating the toll fee.");
        }

    }
    

//sending POST: ASP.NET tries to convert the request body JSON to TollFeeRequest

}


    // på sikt addera en till endpoint som tar emot flera TollFeeRequests
    // try catch - GetTollFee
    // return badRquest