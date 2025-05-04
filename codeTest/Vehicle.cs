using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Vehicle interface declares a single method
public interface Vehicle {
    string GetVehicleType();
}

public class Car : Vehicle {
    public string GetVehicleType() { 
        return "Car"; 
    }
}

public class Motorbike : Vehicle {
    public string GetVehicleType() { 
        return "Motorbike"; 
    }
}
