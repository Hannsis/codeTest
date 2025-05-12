// You need a model class to represent a vehicle in your inventory.

namespace TollAPI.Models;

// this You is a way to describe what kind of vehicle you're dealing with, defining a rule that ALL VEHICLES must adhere to
public interface Vehicle {
    string GetVehicleType();
}

// ------------------------------------------------ // Implementations for each vehicle type // --------------------------------------------------


public class Car : Vehicle {
    public string GetVehicleType() => "Car";
}

public class Motorbike : Vehicle {
    public string GetVehicleType() => "Motorbike";
}

public class Tractor : Vehicle {
    public string GetVehicleType() => "Tractor";
}

public class Emergency : Vehicle {
    public string GetVehicleType() => "Emergency";
}

public class Diplomat : Vehicle {
    public string GetVehicleType() => "Diplomat";
}

public class Foreign : Vehicle {
    public string GetVehicleType() => "Foreign";
}

public class Military : Vehicle {
    public string GetVehicleType() => "Military";
}
