using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types;

[TestSubject(typeof(DateTimeExtensions))]
public static class DateTimeExtensionsTests
{
    public class SetYear
    {
        [Theory]
        [InlineData(2000)]
        [InlineData(2020)]
        [InlineData(2030)]
        public void ChangesYear(int year)
            => new DateTime(2024, 6, 15).SetYear(year).Year.ShouldBe(year);

        [Theory]
        [InlineData(2000)]
        [InlineData(2020)]
        [InlineData(2030)]
        public void PreservesMonthAndDay(int year)
        {
            // Arrange

            // Act
            var result = new DateTime(2024, 6, 15).SetYear(year);

            // Assert
            result.Month.ShouldBe(6);
            result.Day.ShouldBe(15);
        }

        [Fact]
        public void LeapDay_ClipsToFeb28_InNonLeapYear()
            => new DateTime(2024, 2, 29).SetYear(2023).Day.ShouldBe(28);
    }

    public class SetMonth
    {
        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(12)]
        public void ChangesMonth(int month)
            => new DateTime(2024, 3, 15).SetMonth(month).Month.ShouldBe(month);

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(12)]
        public void PreservesYearAndDay(int month)
        {
            // Arrange

            // Act
            var result = new DateTime(2024, 3, 15).SetMonth(month);

            // Assert
            result.Year.ShouldBe(2024);
            result.Day.ShouldBe(15);
        }

        [Fact]
        public void Day31_InShortMonth_ClipsToLastDay()
            => new DateTime(2023, 3, 31).SetMonth(2).Day.ShouldBe(28);
    }

    public class SetDay
    {
        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(28)]
        public void ChangesDay(int day)
            => new DateTime(2024, 6, 15).SetDay(day).Day.ShouldBe(day);

        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(28)]
        public void PreservesYearAndMonth(int day)
        {
            // Arrange

            // Act
            var result = new DateTime(2024, 6, 15).SetDay(day);

            // Assert
            result.Year.ShouldBe(2024);
            result.Month.ShouldBe(6);
        }
    }

    public class SetHour
    {
        [Theory]
        [InlineData(0)]
        [InlineData(12)]
        [InlineData(23)]
        public void ChangesHour(int hour)
            => new DateTime(2024, 6, 15, 10, 30, 45).SetHour(hour).Hour.ShouldBe(hour);

        [Theory]
        [InlineData(0)]
        [InlineData(12)]
        [InlineData(23)]
        public void PreservesMinuteAndSecond(int hour)
        {
            // Arrange

            // Act
            var result = new DateTime(2024, 6, 15, 10, 30, 45).SetHour(hour);

            // Assert
            result.Minute.ShouldBe(30);
            result.Second.ShouldBe(45);
        }
    }

    public class SetMinute
    {
        [Theory]
        [InlineData(0)]
        [InlineData(30)]
        [InlineData(59)]
        public void ChangesMinute(int minute)
            => new DateTime(2024, 6, 15, 10, 30, 45).SetMinute(minute).Minute.ShouldBe(minute);

        [Theory]
        [InlineData(0)]
        [InlineData(30)]
        [InlineData(59)]
        public void PreservesHourAndSecond(int minute)
        {
            // Arrange

            // Act
            var result = new DateTime(2024, 6, 15, 10, 30, 45).SetMinute(minute);

            // Assert
            result.Hour.ShouldBe(10);
            result.Second.ShouldBe(45);
        }
    }

    public class SetSecond
    {
        [Theory]
        [InlineData(0)]
        [InlineData(30)]
        [InlineData(59)]
        public void ChangesSecond(int second)
            => new DateTime(2024, 6, 15, 10, 30, 45).SetSecond(second).Second.ShouldBe(second);

        [Theory]
        [InlineData(0)]
        [InlineData(30)]
        [InlineData(59)]
        public void PreservesMinuteAndMillisecond(int second)
        {
            // Arrange

            // Act
            var result = new DateTime(2024, 6, 15, 10, 30, 45, 500).SetSecond(second);

            // Assert
            result.Minute.ShouldBe(30);
            result.Millisecond.ShouldBe(500);
        }
    }

    public class SetMillisecond
    {
        [Theory]
        [InlineData(0)]
        [InlineData(250)]
        [InlineData(999)]
        public void ChangesMillisecond(int ms)
            => new DateTime(2024, 6, 15, 10, 30, 45, 500).SetMillisecond(ms).Millisecond.ShouldBe(ms);

        [Theory]
        [InlineData(0)]
        [InlineData(250)]
        [InlineData(999)]
        public void PreservesSecond(int ms)
            => new DateTime(2024, 6, 15, 10, 30, 45, 500).SetMillisecond(ms).Second.ShouldBe(45);
    }

    public class SetDate
    {
        [Theory]
        [InlineData(2020, 1, 1)]
        [InlineData(2030, 12, 31)]
        [InlineData(2024, 2, 29)]
        public void ChangesDateParts(int year, int month, int day)
        {
            // Arrange

            // Act
            var result = new DateTime(2024, 6, 15, 10, 30, 45, 500, DateTimeKind.Utc).SetDate(year, month, day);

            // Assert
            result.Year.ShouldBe(year);
            result.Month.ShouldBe(month);
            result.Day.ShouldBe(day);
        }

        [Fact]
        public void PreservesTime()
        {
            // Arrange
            var dt = new DateTime(2024, 6, 15, 10, 30, 45, 500);

            // Act
            var result = dt.SetDate(2020, 1, 1);

            // Assert
            result.Hour.ShouldBe(10);
            result.Minute.ShouldBe(30);
            result.Second.ShouldBe(45);
            result.Millisecond.ShouldBe(500);
        }

        [Fact]
        public void PreservesKind()
            => new DateTime(2024, 6, 15, 10, 30, 45, 500, DateTimeKind.Utc)
                .SetDate(2020, 1, 1).Kind.ShouldBe(DateTimeKind.Utc);
    }

    public class SetTime
    {
        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(12, 30, 45, 500)]
        [InlineData(23, 59, 59, 999)]
        public void ChangesTimeParts(int hour, int minute, int second, int ms)
        {
            // Arrange

            // Act
            var result = new DateTime(2024, 6, 15, 10, 30, 45, 500, DateTimeKind.Utc)
                .SetTime(hour, minute, second, ms);

            // Assert
            result.Hour.ShouldBe(hour);
            result.Minute.ShouldBe(minute);
            result.Second.ShouldBe(second);
            result.Millisecond.ShouldBe(ms);
        }

        [Fact]
        public void PreservesDate()
        {
            // Arrange
            var dt = new DateTime(2024, 6, 15);

            // Act
            var result = dt.SetTime(8, 0);

            // Assert
            result.Year.ShouldBe(2024);
            result.Month.ShouldBe(6);
            result.Day.ShouldBe(15);
        }

        [Fact]
        public void DefaultsSecondAndMillisecondToZero()
        {
            // Arrange
            var dt = new DateTime(2024, 6, 15, 10, 30, 45, 500);

            // Act
            var result = dt.SetTime(8, 0);

            // Assert
            result.Second.ShouldBe(0);
            result.Millisecond.ShouldBe(0);
        }

        [Fact]
        public void PreservesKind()
            => new DateTime(2024, 6, 15, 10, 30, 45, 500, DateTimeKind.Utc)
                .SetTime(0, 0).Kind.ShouldBe(DateTimeKind.Utc);
    }
}
