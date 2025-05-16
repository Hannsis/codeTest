using TollAPI.Models;
using TollAPI.Services;
using Xunit; 
using Moq;


namespace TollFee.Api.Tests;

public class TollCalculatorTests
{

    // MethodUnderTest_Scenario_ExpectedResult
    [Fact]
    public void GetTollFee_SinglePass_Billable_ReturnsFee()
    {
        // Arrange
        // Create a mock of the abstract/interface Vehicle
        var mockVehicle = new Mock<Vehicle>();

        // Set up its IsTollFree property to false (dvs a car/motorbike wtv that pays toll, billable vehicle):
        //when value set to truee rror Message: Assert.Equal() Failure: Values differ - Expected:18 -  Actual:0
        mockVehicle.SetupGet(v => v.IsTollFree).Returns(false);
        var vehicle = mockVehicle.Object;

        var date = new DateTime(2025, 5, 16, 7, 15, 0); // 7:15 => fee 18
        // instansiera calculator - which runs the calculations.. duh
        var calculator = new TollCalculator();

        // Act
        var total = calculator.GetTollFee(vehicle, new[] { date });

        // Assert
        Assert.Equal(18, total);
    }

    //writing test to test also true for toll free vehicle
    [Fact]
    public void GetTollFee_SinglePass_NotBillable_ReturnsFee()
    {
        // Arrange
        var mockVehicle = new Mock<Vehicle>();
        mockVehicle.SetupGet(v => v.IsTollFree).Returns(true);
        var vehicle = mockVehicle.Object;

        var date = new DateTime(2025, 5, 16, 7, 15, 0); // 7:15 => fee 18 - but fee should return noll pga NOT BILLABLE
        var calculator = new TollCalculator();

        // Act
        var total = calculator.GetTollFee(vehicle, new[] { date });

        // Assert
        Assert.Equal(0, total);
    }

    }

