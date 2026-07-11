namespace Vulcan.Extensions;

public static class DateTimeExtensions
{
    extension(DateTime dt)
    {
        /// <summary>Sets the Year part of the DateTime</summary>
        /// <remarks>
        /// - The year is clamped to 1..9999.
        /// - If the day is not valid in the target year (Feb 29), the last valid day of the month is returned instead.
        /// </remarks>
        public DateTime SetYear(int year)
            => dt.AddYears(Clamp(year, 1, 9999) - dt.Year);

        /// <summary>Sets the Month part of the DateTime</summary>
        /// <remarks>
        /// - The month is clamped to 1..12.
        /// - If the day is not valid in the target month, the last valid day of the month is returned instead.
        /// </remarks>
        public DateTime SetMonth(int month)
            => dt.AddMonths(Clamp(month, 1, 12) - dt.Month);

        /// <summary>Sets the Day part of the DateTime</summary>
        /// <remarks>The day is clamped to 1..(days in the month).</remarks>
        public DateTime SetDay(int day)
            => dt.AddDays(Clamp(day, 1, DateTime.DaysInMonth(dt.Year, dt.Month)) - dt.Day);

        /// <summary>Sets the Hour part of the DateTime</summary>
        /// <remarks>
        /// - The hour is clamped to 0..23.
        /// - DaylightSaving-Time changes are ignored. 01:00 to 6:00 is always 06:00
        /// </remarks>
        public DateTime SetHour(int hour)
            => dt.AddHours(Clamp(hour, 0, 23) - dt.Hour);

        /// <summary>Sets the Minute part of the DateTime</summary>
        /// <remarks>The minute is clamped to 0..59.</remarks>
        public DateTime SetMinute(int minute)
            => dt.AddMinutes(Clamp(minute, 0, 59) - dt.Minute);

        /// <summary>Sets the Second part of the DateTime</summary>
        /// <remarks>The second is clamped to 0..59.</remarks>
        public DateTime SetSecond(int second)
            => dt.AddSeconds(Clamp(second, 0, 59) - dt.Second);

        /// <summary>Sets the Millisecond part of the DateTime</summary>
        /// <remarks>The millisecond is clamped to 0..999.</remarks>
        public DateTime SetMillisecond(int millisecond)
            => dt.AddMilliseconds(Clamp(millisecond, 0, 999) - dt.Millisecond);

        /// <summary>Sets the date without changing the time</summary>
        /// <remarks>Each part is clamped to its valid range: year 1..9999, month 1..12, day 1..(days in the month).</remarks>
        public DateTime SetDate(int year, int month, int day)
            => dt.SetYear(year).SetMonth(month).SetDay(day);

        /// <summary>Sets the date (read from passed DateTime) without changing the time</summary>
        /// <remarks>The date reading from argument is done as-is. No timezone conversion is done </remarks>
        public DateTime SetDate(DateTime dateProvider)
            => new(dateProvider.Year, dateProvider.Month, dateProvider.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, dt.Kind);

        /// <summary>Sets the time without changing the date</summary>
        /// <remarks>
        /// - Each part is clamped to its valid range: hour 0..23, minute 0..59, second 0..59, millisecond 0..999.
        /// - DaylightSaving-Time changes are ignored. 01:00 to 6:00 is always 06:00
        /// </remarks>
        public DateTime SetTime(int hour, int minute, int second = 0, int millisecond = 0)
            => new(dt.Year, dt.Month, dt.Day, Clamp(hour, 0, 23), Clamp(minute, 0, 59), Clamp(second, 0, 59), Clamp(millisecond, 0, 999), dt.Kind);

        /// <summary>Sets the time (read from passed DateTime) without changing the date</summary>
        /// <remarks>
        /// - DaylightSaving-Time changes are ignored. 01:00 to 6:00 is always 06:00
        /// - The time reading from argument is done as-is. No timezone conversion is done
        /// </remarks>
        public DateTime SetTime(DateTime timeProvider)
            => new(dt.Year, dt.Month, dt.Day, timeProvider.Hour, timeProvider.Minute, timeProvider.Second, timeProvider.Millisecond, dt.Kind);
    }

    // Math.Clamp is not available on netstandard2.0
    static int Clamp(int value, int min, int max)
        => Math.Max(min, Math.Min(value, max));
}
