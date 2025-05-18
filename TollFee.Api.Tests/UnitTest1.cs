using Newtonsoft.Json;
using TollAPI.Models;
using TollAPI.Services;
using Xunit; 
using Moq;

namespace TollFee.Api.Tests
{
    public class TollCalculatorTests
    {
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
            var docs = JsonConvert.DeserializeObject<List<Within60Entry>>(json);
            return docs.Select(d => new object[]
            {
                d.Times
                .Select(t => new DateTime(2025, t.Month, t.Day, t.Hour, t.Minute, 0))
                .ToArray(),
                d.ExpectedFee
            });
        }

        private class TimeEntry
        {
            public int Month { get; set; }
            public int Day { get; set; }
            public int Hour { get; set; }
            public int Minute { get; set; }
            public int Fee { get; set; }
        }
        private class Within60Entry
        {
            public List<TimeEntry> Times { get; set; }
            public int ExpectedFee { get; set; }
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

        // public void GetTollFee_MultiplOutsideSixtyMinutes_CalculateFee()
        // GetTollFee_SinglePass_BeforeChargeWindow_ReturnsZero         Pass at e.g. 05:59 on a weekday; expect 0.
        // GetTollFee_SinglePass_AtEachBoundary_ReturnsCorrectFee        tex. 06:00 → 8, 06:29 → 8, 06:30 → 13
        // GetTollFee_ExceedsDailyCap_ReturnsSixty
        // GetTollFee_ExceedsDailyCap_ReturnsSixty
        // Test a hard‐coded 2013 holiday
        // Provide dates out of order but within 60 min; verify correct highest‐fee logic.
        // dates out of order in general 
    }
}
