// You need a model class to represent a vehicle in your inventory.

namespace TollAPI.Models;

// this You is a way to describe what kind of vehicle you're dealing with, defining a rule that ALL VEHICLES must adhere to
public interface Vehicle {
    string GetVehicleType();
}

// this class defines vehicle car
public class Car : Vehicle {
    public string GetVehicleType() { 
        return "Car"; 
    }
}

// this class defines vehicle motorbike
public class Motorbike : Vehicle {
    public string GetVehicleType() { 
        return "Motorbike"; 
    }
}

