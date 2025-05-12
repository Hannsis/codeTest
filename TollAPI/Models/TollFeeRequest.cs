// to enable FromBody 
// deseriaialized input - errors when doing dotnet run 
// DTO 
// this class defines a request for calculating the toll fee. 

namespace TollAPI.Models;
public class TollFeeRequest
{
    public string VehicleType { get; set; }
    public DateTime[] Dates { get; set; }
}