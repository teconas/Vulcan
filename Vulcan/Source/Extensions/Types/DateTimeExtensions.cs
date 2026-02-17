namespace Vulcan.Extensions;

public static class DateTimeExtensions
{
    extension(DateTime dt)
    {
        /// <summary>Sets the Year part of the DateTime</summary>
        /// <remark>If the new date is not valid on the target year, the latest earlier valid date is returned instead.</remark>
        public DateTime SetYear(int year)
            => dt.AddYears(year - dt.Year);

        /// <summary>Sets the Month part of the DateTime</summary>
        /// <remark>If the new date is not valid on the target month, the latest earlier valid date is returned instead.</remark>
        public DateTime SetMonth(int month)
            => dt.AddMonths(month - dt.Month);

        /// <summary>Sets the Month part of the DateTime</summary>
        /// <remark>If the new day is not valid on the target, the latest earlier valid date is returned instead.</remark>
        public DateTime SetDay(int day)
            => dt.AddDays(day - dt.Day);

        /// <summary>Sets the Selected part of the DateTime.</summary>
        /// <remark>DaylightSaving-Time changes are ignore. 01:00 to 6:00 is always 06:00</remark>
        public DateTime SetHour(int hour)
            => dt.AddHours(hour - dt.Hour);

        /// <summary>Sets the Selected part of the DateTime.</summary>
        public DateTime SetMinute(int minute)
            => dt.AddMinutes(minute - dt.Minute);

        /// <summary>Sets the Selected part of the DateTime.</summary>
        public DateTime SetSecond(int second)
            => dt.AddSeconds(second - dt.Second);

        /// <summary>Sets the Selected part of the DateTime.</summary>
        public DateTime SetMillisecond(int millisecond)
            => dt.AddMilliseconds(millisecond - dt.Millisecond);

        /// <summary>Sets the date without changing the time</summary>
        public DateTime SetDate(int year, int month, int day)
            => new(year, month, day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, dt.Kind);

        /// <summary>Sets the time without changing the date</summary>
        /// <remark>DaylightSaving-Time changes are ignore. 01:00 to 6:00 is always 06:00</remark>
        public DateTime SetTime(int hour, int minute, int second = 0, int millisecond = 0)
            => new(dt.Year, dt.Month, dt.Day, hour, minute, second, millisecond, dt.Kind);
    }
}