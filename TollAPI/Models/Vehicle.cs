// You need a model class to represent a vehicle in your inventory.

namespace TollAPI.Models;

// this You is a way to describe what kind of vehicle you're dealing with, defining a rule that ALL VEHICLES must adhere to
public interface Vehicle {
    string GetVehicleType();
    bool IsTollFree { get; }
    
}

// ------------------------------------------------ // Implementations for each vehicle type // --------------------------------------------------


public class Car : Vehicle
{
    public string GetVehicleType() => "Car";
    public bool IsTollFree => false;
}

public class Motorbike : Vehicle
{
    public string GetVehicleType() => "Motorbike";
    public bool IsTollFree => false;
}

public class Tractor : Vehicle
{
    public string GetVehicleType() => "Tractor";
    public bool IsTollFree => true;
}

public class Emergency : Vehicle
{
    public string GetVehicleType() => "Emergency";
    public bool IsTollFree => true;
}

public class Diplomat : Vehicle
{
    public string GetVehicleType() => "Diplomat";
    public bool IsTollFree => true;
}

public class Foreign : Vehicle
{
    public string GetVehicleType() => "Foreign";
    public bool IsTollFree => true;
}

public class Military : Vehicle
{
    public string GetVehicleType() => "Military";
    public bool IsTollFree => true;
}
