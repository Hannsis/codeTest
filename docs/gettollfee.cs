public int GetTollFee(IVehicle vehicle, DateTime[] dates)
{
    if (vehicle == null) 
        throw new ArgumentNullException(nameof(vehicle));

    if (dates == null || dates.Length == 0) 
        return 0;

    Array.Sort(dates);
    DateTime windowStart = dates[0];
    int windowMaxFee = GetTollFee(dates[0], vehicle);
    int totalFee = 0;

    foreach (var date in dates.Skip(1))
    {
        int fee = GetTollFee(date, vehicle);
        double minutes = (date - windowStart).TotalMinutes;

        if (minutes <= 60)
        {
            windowMaxFee = Math.Max(windowMaxFee, fee);
        }
        else
        {
            totalFee += windowMaxFee;
            windowStart = date;
            windowMaxFee = fee;
        }
    }

    totalFee += windowMaxFee;
    return Math.Min(totalFee, 60);
}
