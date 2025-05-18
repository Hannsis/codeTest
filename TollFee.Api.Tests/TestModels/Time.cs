namespace TollFeeAPI.Tests.TestModels;
        internal class TimeEntry
        {
            public int Month { get; set; }
            public int Day { get; set; }
            public int Hour { get; set; }
            public int Minute { get; set; }
            public int Fee { get; set; }
        }
        internal class Within60Entry
        {
            public List<TimeEntry> Times { get; set; }
            public int ExpectedFee { get; set; }
        }
