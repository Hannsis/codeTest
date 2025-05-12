using TollAPI.Models;

namespace TollAPI.Services;
public class VehicleFactory
{
    public Vehicle CreateVehicle(string vehicleType)
    {
        return vehicleType.ToLower() switch
        {
            "car" => new Car(),
            "motorbike" => new Motorbike(),
            "tractor" => new Tractor(),
            "emergency" => new Emergency(),
            "diplomat" => new Diplomat(),
            "foreign" => new Foreign(),
            "military" => new Military(),
            _ => throw new ArgumentException($"Unsupported vehicle type: {vehicleType}")
        };
    }
}
