using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


[ApiController]
[Route("api/toll-events")]
public class TollEventsController : ControllerBase
{
    private readonly ITollEventService _svc;
    public TollEventsController(ITollEventService svc) => _svc = svc;

    // POST api/toll-events
    [HttpPost]
    public async Task<IActionResult> Record([FromBody] TollEventDto dto)
    {
        await _svc.RecordPassageAsync(dto.VehicleId, dto.Timestamp);
        return Ok();
    }

    // GET api/toll-events/{vehicleId}/fee?date=2025-05-04
    [HttpGet("{vehicleId}/fee")]
    public Task<int> GetFee(Guid vehicleId, [FromQuery] DateTimeOffset date)
        => _svc.GetDailyFeeAsync(vehicleId, date);
}
