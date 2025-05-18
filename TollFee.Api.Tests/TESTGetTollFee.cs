// XUNIT - nuggies
// https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
// WHY?!
// changes: code breaks or introduce bugs
// unit test on what is constantly changing or critical to the app
//
// [Fact] for single-scenario tests.
// [Theory] + [InlineData] for parameterized cases.




// [Test]
// private void TestGetTollFee() 
// {
//     // Arrange - Declare variables and objects needed for testing
//      var dates1 = List<DateTime>() {}
//      var dates2 = List<DateTime>() {}

//     // Act - Perform test using declared items
//      var result1 = TollCalculator.GetTollFee(vehicle, dates1)
//      var result2 = TollCalculator.GetTollFee(vehicle, dates2)
//     
//     // Assert - Compare results to see if output is correct
//     Assert.Equals(result1 (något förväntat resultat))
//     Assert.Equals(result2 (något förväntat resultat))
// }



// dotnet new xunit -n TollFee.Api.Tests
// dotnet watch test
// cd TollFee.Api.Tests
// dotnet test --logger "console;verbosity=detailed"


//Moq is a mocking library you can use to fake interfaces—only necessary if your code under test depends on other services via interfaces.