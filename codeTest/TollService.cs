using Microsoft.EntityFrameworkCore;
using System.Linq;


public interface ITollEventService
{
    Task RecordPassageAsync(Guid vehicleId, DateTimeOffset timestamp);
    Task<int>  GetDailyFeeAsync(Guid vehicleId, DateTimeOffset date);
}

public class TollEventService : ITollEventService
{
    private readonly ApplicationDbContext _db;
    private readonly TollCalculator      _calculator;

    public TollEventService(ApplicationDbContext db, TollCalculator calculator)
    {
        _db         = db;
        _calculator = calculator;
    }

    public async Task RecordPassageAsync(Guid vehicleId, DateTimeOffset timestamp)
    {
        var ev = new TollEvent { VehicleId = vehicleId, Timestamp = timestamp };
        _db.TollEvents.Add(ev);
        await _db.SaveChangesAsync();
    }

    public async Task<int> GetDailyFeeAsync(Guid vehicleId, DateTimeOffset date)
    {
        // 1) fetch all timestamps for that vehicle on that calendar day
        var dayStart = date.Date;
        var dayEnd   = dayStart.AddDays(1);
        var timestamps = await _db.TollEvents
            .Where(e => e.VehicleId == vehicleId
                     && e.Timestamp >= dayStart
                     && e.Timestamp <  dayEnd)
            .Select(e => e.Timestamp.UtcDateTime)
            .ToArrayAsync();

        // 2) hand them (and a dummy Vehicle instance) to your calculator
        var vehicle = await _db.Vehicles.FindAsync(vehicleId);
        return _calculator.GetTollFee(vehicle, timestamps);
    }
}
