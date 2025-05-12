// to enable FromBody 
// deseriaialized input - errors when doing dotnet run 
// System.NotSupportedException: Deserialization of interface or abstract types is not supported. Type 'TollAPI.Models.Vehicle'

// DTO - data transfer object

namespace TollAPI.Models;
// this class defines a request for calculating the toll fee. 
// public class TollFeeRequest
// {
//     public Vehicle Vehicle { get; set; }
//     public DateTime[] Dates { get; set; }
// }

public class TollFeeRequest
{
    public string VehicleType { get; set; }
    public DateTime[] Dates { get; set; }
}