# TollCalculatorTests

The `TollCalculatorTests` class uses xUnit and Moq to validate the behavior of the `TollCalculator` service. It covers:

* **Holiday checks**: Ensuring no fees on holidays.
* **Boundary values**: Verifying correct fees at time interval boundaries.
* **Single and multiple passes**: Testing individual passages and groups within and outside the 60‑minute rule.
* **Daily cap enforcement**: Confirming the maximum daily fee is 60 SEK.
* **Out‑of‑order inputs**: Handling unsorted timestamps correctly.

Test data is supplied via JSON files in `TestData`, deserialized into `TimeEntry` and `MultiplePasses` models. 
MemberData attributes feed these into parameterized tests.

---

## Tests
make sure you are using the correct models depending on what type of test you wish to use

### `GetTollFee_SinglePass_NotBillable_ReturnsZero`
Verifies that toll-free vehicles are not billable.

### `GetTollFee_NoPasses_ReturnsZero`
Ensures no charges when no valid timestamps are provided.

### `GetTollFee_SinglePass_AtEachBoundary_ReturnsCorrectFee`
Verifies the calculator correctly applies boundary rules at the edges of each fee interval. Fee equals specified boundary fee.

### `GetTollFee_PassesOutOfOrder_CalculatesCorrectFee`
Checks proper sorting and application of the 60‑minute highest‑fee rule when timestamps are unordered. Total fee matches expected.

### `GetTollFee_MultipleWithinSixtyMinutes_ChargesHighestFeeOnly`
Tests that within any 60‑minute window, only the highest single fee is charged. Fee equals the highest charge in each group.

### `GetTollFee_MultiplOutsideSixtyMinutes_CalculateFee`
Ensures separate windows beyond 60 minutes are each charged independently. Sum of fees across windows matches expected.

### `GetTollFee_HolidayTests_ReturnsZero`
Confirms that passages on holidays yield no fee. Fee is 0 for each holiday timestamp.

### `GetTollFee_ExceedsDailyCap_ReturnsSixty`
 Validates that the daily maximum of 60 SEK is enforced. Total fee is capped at 60 SEK regardless of cumulative passages.

