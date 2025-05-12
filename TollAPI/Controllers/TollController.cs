using Microsoft.AspNetCore.Mvc;
using TollAPI.Models;
using TollAPI.Services;


namespace TollAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TollEventsController : ControllerBase
{
   
    // dependancies for all the http requests
    private readonly TollCalculator _calculator;
    private readonly VehicleFactory _vehicleFactory;
    
    // constructor injection
    public TollEventsController(TollCalculator calculator, VehicleFactory vehicleFactory)
    {
        _calculator = calculator;
        _vehicleFactory = vehicleFactory;
    }

    [HttpPost("getTollFee")] 
    public async Task<IActionResult> Record([FromBody]TollFeeRequest TollFeeRequest)
    
    {
    try
    {
        // validate Input
        if (TollFeeRequest == null || string.IsNullOrWhiteSpace(TollFeeRequest.VehicleType) || TollFeeRequest.Dates == null)
            return BadRequest("Request, vehicleType, and dates are required.");
        
        var vehicle = _vehicleFactory.CreateVehicle(TollFeeRequest.VehicleType);
        int totalFee = _calculator.GetTollFee(vehicle, TollFeeRequest.Dates);
        return Ok($"Toll fee calculated: {totalFee}, vehicle: {vehicle.GetVehicleType()}, dates passed: {string.Join(", ", TollFeeRequest.Dates)}");

    }

    catch (ArgumentException ex)
    // someone giving weird input
        {
            // returns a 400 - dvs client side error
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            // returns 500 errpr - dvs serverside error
            return StatusCode(500, "Something went wrong while calculating the toll fee.");
        }

    }
    

//sending POST: ASP.NET tries to convert the request body JSON to TollFeeRequest

}


    // på sikt addera en till endpoint som tar emot flera TollFeeRequests
    // try catch - GetTollFee
    // return badRquest