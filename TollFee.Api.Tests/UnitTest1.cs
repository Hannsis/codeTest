using TollAPI.Models;
using TollAPI.Services;
using Xunit; 
using Moq;


namespace TollFee.Api.Tests;

public class TollCalculatorTests
{

    // start with tesing that the vehicle is tollfree when true
    [Fact]
    public void GetTollFee_SinglePass_NotBillable_ReturnsZero()
    // MethodUnderTest_Scenario_ExpectedResult
    {
        // Arrange
        var mockVehicle = new Mock<Vehicle>();
        // Create a mock of the abstract/interface Vehicle
        mockVehicle.SetupGet(v => v.IsTollFree).Returns(true);
        var vehicle = mockVehicle.Object;

        var date = new DateTime(2025, 5, 16, 7, 15, 0); // 7:15 => fee 18 - but fee should return noll pga NOT BILLABLE
        var calculator = new TollCalculator();

        // Act
        var total = calculator.GetTollFee(vehicle, new[] { date });

        // Assert
        Assert.Equal(0, total);
    }

    [Fact]
    public void GetTollFee_SinglePass_Billable_ReturnsFee()
    // MethodUnderTest_Scenario_ExpectedResult
    {
        // Arrange
        var mockVehicle = new Mock<Vehicle>();

        // Set up its IsTollFree property to false (dvs a car/motorbike wtv that pays toll, billable vehicle)
        mockVehicle.SetupGet(v => v.IsTollFree).Returns(false);
        var vehicle = mockVehicle.Object;

        var date = new DateTime(2025, 5, 16, 7, 15, 0); // 7:15 => fee 18
        var calculator = new TollCalculator();

        // Act
        // literally passing variables above into GetTollFee method and checking that the result is what it is
        var total = calculator.GetTollFee(vehicle, new[] { date });

        // Assert
        Assert.Equal(18, total);
    }
    // TODO - look into bool for true/false, not two seperate tests. But i'll do this when i have more tests which are working already 


    //test many passes within 60 minutes
    // shoudl give result with most expensive

    [Fact]
    public void GetTollFee_MultipleWithinSixtyMinutes_ChargesHighestFeeOnly()
    {
        // Arrange
        var mockVehicle = new Mock<Vehicle>();
        mockVehicle.SetupGet(v => v.IsTollFree).Returns(false);
        var vehicle = mockVehicle.Object;

        var times = new[]
        {
            new DateTime(2025, 5, 9, 7, 0, 0),   // 2025-05-09 07:00 -> fee 18
            new DateTime(2025, 5, 9, 8, 0, 0)    // 2025-05-09 08:00 -> fee 13 (within 60 minutes)
        };
        var calculator = new TollCalculator();

        // Act
        var total = calculator.GetTollFee(vehicle, times);

        // Assert
        Assert.Equal(18, total);
    }
    
    [Fact]
    public void GetTollFee_NoPasses_ReturnsZero()
    {
        // Arrange
        var mockVehicle = new Mock<Vehicle>();
        mockVehicle.SetupGet(v => v.IsTollFree).Returns(false);
        var vehicle = mockVehicle.Object;

        var times = new[]
        {
            new DateTime()
        };
        var calculator = new TollCalculator();

        // Act
        var total = calculator.GetTollFee(vehicle, times);

        // Assert
        Assert.Equal(0, total);
    }

    
    [Fact]
    public void GetTollFee_NullVehicle_ReturnsZero()
    {
        // Arrange
        var mockVehicle = new Mock<Vehicle>();
        mockVehicle.SetupGet(v => v.IsTollFree).Returns(null);
        var vehicle = mockVehicle.Object;

        var times = new[]
        {
            new DateTime(2025, 5, 9, 7, 0, 0),
            new DateTime(2025, 5, 9, 8, 0, 0)
        };
        var calculator = new TollCalculator();

        // Act
        var total = calculator.GetTollFee(vehicle, times);

        // Assert
        Assert.Equal(0, total);
    }
    // TEST FAILING

    [Fact]
    public void GetTollFee_MultiplOutsideSixtyMinutes_CalculateFee()
    {
        // Arrange
        var mockVehicle = new Mock<Vehicle>();
        mockVehicle.SetupGet(v => v.IsTollFree).Returns(false);
        var vehicle = mockVehicle.Object;

        var times = new[]
        {
            new DateTime(2025, 5, 9, 7, 0, 0),   // 2025-05-09 07:00 -> fee 18
            new DateTime(2025, 5, 9, 9, 0, 0)    // 2025-05-09 09:00 -> fee 8 (within 60 minutes)
        };
        var calculator = new TollCalculator();

        // Act
        var total = calculator.GetTollFee(vehicle, times);

        // Assert
        Assert.Equal(26, total);
    }
    // this test is returning error: 
    // Error Message: Assert.Equal() Failure: Values differ
    //   Expected: 26
    //   Actual:   18
    //   e: Values differ
    //   Expected: 26
    //   Actual:   18

 
    //TODO: fix logic so tests pass
    
    // GetTollFee_SinglePass_BeforeChargeWindow_ReturnsZero         Pass at e.g. 05:59 on a weekday; expect 0.
    // GetTollFee_SinglePass_AtEachBoundary_ReturnsCorrectFee        tex. 06:00 → 8, 06:29 → 8, 06:30 → 13
    // GetTollFee_ExceedsDailyCap_ReturnsSixty
    // GetTollFee_ExceedsDailyCap_ReturnsSixty
    // Test a hard‐coded 2013 holiday
    // Provide dates out of order but within 60 min; verify correct highest‐fee logic.
    // dates out of order in general 

}



