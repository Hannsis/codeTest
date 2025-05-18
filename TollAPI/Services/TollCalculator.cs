using TollAPI.Models;


namespace TollAPI.Services;
//main class
public class TollCalculator
{
    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        if (vehicle.IsTollFree || dates == null || dates.Length == 0)
            return 0;
            
        var sortedDates = dates.OrderBy(d => d).ToArray();

        DateTime windowStart  = sortedDates[0];
        int      windowMaxFee = GetTollFee(windowStart, vehicle);
        int      totalFee     = windowMaxFee;
        
        foreach (var date in sortedDates.Skip(1))
        {
            int fee = GetTollFee(date, vehicle);
            double mins = (date - windowStart).TotalMinutes;

            if (mins <= 60)
            {
                if (fee > windowMaxFee)
                {
                    totalFee = totalFee - windowMaxFee + fee;
                    windowMaxFee = fee;
                }
            }
            else
            {
                totalFee += fee;
                windowStart = date;
                windowMaxFee = fee;
            }
        }
        return Math.Min(totalFee, 60);
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    // Any date passing either check is toll‐free
    // returns true for weekeds and holiday dates (hardcoded)
    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;
        // hardcoded year(?) future fixes remove hardcoded, add feature which uses a library? probs exists a library with dates and holidays
        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        return vehicle.IsTollFree;
    }

    //check exemption types
    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}