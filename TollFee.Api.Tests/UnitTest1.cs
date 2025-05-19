using Newtonsoft.Json;
using TollAPI.Models;
using TollAPI.Services;
using Xunit; 
using Moq;
using TollFeeAPI.Tests.TestModels;

namespace TollFee.Api.Tests
{
    public class TollCalculatorTests
    {
        public static IEnumerable<object[]> HolidayTests()
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "TestData", "HolidayTests.json");
            var json = File.ReadAllText(jsonPath);
            var docs = JsonConvert.DeserializeObject<List<TimeEntry>>(json);

            return docs.Select(d => new object[]
            {
                new DateTime(2013, d.Month, d.Day, d.Hour, d.Minute, 0),
                d.Fee
            });
        }

                public static IEnumerable<object[]> TollBoundaries()
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "TestData", "TollBoundaries.json");
            var json = File.ReadAllText(jsonPath);
            var docs = JsonConvert.DeserializeObject<List<TimeEntry>>(json);

            return docs.Select(d => new object[]
            {
                new DateTime(2013, d.Month, d.Day, d.Hour, d.Minute, 0),
                d.Fee
            });
        }
        public static IEnumerable<object[]> TestTimes()
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "TestData", "SingelPassTest.json");
            var json = File.ReadAllText(jsonPath);
            var docs = JsonConvert.DeserializeObject<List<TimeEntry>>(json);
            return docs.Select(d => new object[]
            {
                new DateTime(2013, d.Month, d.Day, d.Hour, d.Minute, 0),
                d.Fee
            });
        }

        public static IEnumerable<object[]> WithinSixtyData()
        {
            var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "TestData", "within60Tests.json"));
            var docs = JsonConvert.DeserializeObject<List<MultiplePasses>>(json);
            return docs.Select(d => new object[]
            {
                d.Times
                .Select(t => new DateTime(2013, t.Month, t.Day, t.Hour, t.Minute, 0))
                .ToArray(),
                d.ExpectedFee
            });
        }

        public static IEnumerable<object[]> OutsideSixtyData()
        {
            var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "TestData", "Outside60Tests.json"));
            var docs = JsonConvert.DeserializeObject<List<MultiplePasses>>(json);
            return docs.Select(d => new object[]
            {
                d.Times
                .Select(t => new DateTime(2013, t.Month, t.Day, t.Hour, t.Minute, 0))
                .ToArray(),
                d.ExpectedFee
            });
        }
        
        public static IEnumerable<object[]> ExceededHighestCap()
        {
            var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "TestData", "ExceededHighestCap.json"));
            var docs = JsonConvert.DeserializeObject<List<MultiplePasses>>(json);
            return docs.Select(d => new object[]
            {
                d.Times
                .Select(t => new DateTime(2013, t.Month, t.Day, t.Hour, t.Minute, 0))
                .ToArray(),
                d.ExpectedFee
            });
        }

        [Theory]
        [MemberData(nameof(TestTimes))]
        public void GetTollFee_SinglePass_Billable_ReturnsFee(DateTime time, int expectedFee)
        {
            // Arrange
            var vehicle = new Mock<Vehicle>();
            vehicle.SetupGet(v => v.IsTollFree).Returns(false);
            var calculator = new TollCalculator();

            // Act
            var total = calculator.GetTollFee(vehicle.Object, new[] { time });

            // Assert
            Assert.Equal(expectedFee, total);
        }

        [Theory]
        [MemberData(nameof(TollBoundaries))]
        public void GetTollFee_SinglePass_AtEachBoundary_ReturnsCorrectFee (DateTime time, int expectedFee)
        {
            // Arrange
            var vehicle = new Mock<Vehicle>();
            vehicle.SetupGet(v => v.IsTollFree).Returns(false);
            var calculator = new TollCalculator();

            // Act
            var total = calculator.GetTollFee(vehicle.Object, new[] { time });

            // Assert
            Assert.Equal(expectedFee, total);
        }

        [Fact]
        public void GetTollFee_SinglePass_NotBillable_ReturnsZero()
        {
            // Arrange
            var mockVehicle = new Mock<Vehicle>();
            mockVehicle.SetupGet(v => v.IsTollFree).Returns(true);
            var vehicle = mockVehicle.Object;
            var date = new DateTime(2025, 5, 16, 7, 15, 0);
            var calculator = new TollCalculator();

            // Act
            var total = calculator.GetTollFee(vehicle, new[] { date });

            // Assert
            Assert.Equal(0, total);
        }

        [Theory]
        [MemberData(nameof(WithinSixtyData))]
        public void GetTollFee_MultipleWithinSixtyMinutes_ChargesHighestFeeOnly(
            DateTime[] times, int expectedFee)
        {
            var mockVehicle = new Mock<Vehicle>();
            mockVehicle.SetupGet(v => v.IsTollFree).Returns(false);
            var calculator = new TollCalculator();

            var total = calculator.GetTollFee(mockVehicle.Object, times);
            Assert.Equal(expectedFee, total);
        }

        [Theory]
        [MemberData(nameof(OutsideSixtyData))]
        public void GetTollFee_MultiplOutsideSixtyMinutes_CalculateFee(
            DateTime[] times, int expectedFee)
        {
            var mockVehicle = new Mock<Vehicle>();
            mockVehicle.SetupGet(v => v.IsTollFree).Returns(false);
            var calculator = new TollCalculator();

            var total = calculator.GetTollFee(mockVehicle.Object, times);
            Assert.Equal(expectedFee, total);
        }

        [Theory]
        [MemberData(nameof(HolidayTests))]
        public void GetTollFee_HolidayTests_ReturnsZero(DateTime time, int expectedFee)
        {
            // Arrange
            var vehicle = new Mock<Vehicle>();
            vehicle.SetupGet(v => v.IsTollFree).Returns(false);
            var calculator = new TollCalculator();

            // Act
            var total = calculator.GetTollFee(vehicle.Object, new[] { time });

            // Assert
            Assert.Equal(expectedFee, total);
        }
        
        [Theory]
        [MemberData(nameof(ExceededHighestCap))]
        public void GetTollFee_ExceedsDailyCap_ReturnsSixty(DateTime[] times, int expectedFee)
        {
            var mockVehicle = new Mock<Vehicle>();
            mockVehicle.SetupGet(v => v.IsTollFree).Returns(false);
            var calculator = new TollCalculator();

            var total = calculator.GetTollFee(mockVehicle.Object, times);
            Assert.Equal(expectedFee, total);
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

        // GetTollFee_SinglePass_BeforeChargeWindow_ReturnsZero         Pass at e.g. 05:59 on a weekday; expect 0.
 
        // Provide dates out of order but within 60 min; verify correct highest‐fee logic.
        // dates out of order in general 


    }
}
