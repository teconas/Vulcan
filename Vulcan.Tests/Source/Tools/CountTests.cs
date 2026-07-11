using System.Collections;

namespace Vulcan.Tests.Tools;

public static class CountTests
{
    public class CountUp
    {
        [Theory]
        [InlineData(0, 5, 1, 0, 1, 2, 3, 4, 5)]
        [InlineData(1, 5, 1, 1, 2, 3, 4, 5)]
        [InlineData(-5, 11, 3, -5, -2, 1, 4, 7, 10)]
        public void FromTo(int from, int to, int step, params int[] expect)
        {
            // Arrange

            // Act
            var count = Count.Up(from).To(to);
            var result = (step is 1 ? count : count.Step(step)).ToArray();

            // Assert
            result.ShouldBe(expect);
        }

        [Theory]
        [InlineData(0, 6, 1, 0, 1, 2, 3, 4, 5)]
        [InlineData(5, 6, 1, 5, 6, 7, 8, 9, 10)]
        [InlineData(-10, 6, 3, -10, -7, -4, -1, 2, 5)]
        public void Take(int from, int take, int step, params int[] expect)
        {
            // Arrange

            // Act
            var count = Count.Up(from).Take(take);
            var result = (step is 1 ? count : count.Step(step)).ToArray();

            // Assert
            result.ShouldBe(expect);
        }

        [Theory]
        [InlineData(0, 1, 0, 1, 2, 3, 4, 5)]
        [InlineData(-5, 1, -5, -4, -3, -2, -1, 0, 1)]
        [InlineData(-10, 3, -10, -7, -4, -1, 2, 5)]
        public void Endless(int from, int step, params int[] expect)
        {
            // Arrange

            // Act
            var count = Count.Up(from);
            var result = (step is 1 ? count : count.Step(step)).Select(x => x).Take(expect.Length).ToArray();

            // Assert
            result.ShouldBe(expect);
        }
    }

    public class CountDown
    {
        [Theory]
        [InlineData(0, -5, 1, 0, -1, -2, -3, -4, -5)]
        [InlineData(5, 0, 1, 5, 4, 3, 2, 1, 0)]
        [InlineData(10, -6, 3, 10, 7, 4, 1, -2, -5)]
        public void FromTo(int from, int to, int step, params int[] expect)
        {
            // Arrange

            // Act
            var count = Count.Down(from).To(to);
            var result = (step is 1 ? count : count.Step(step)).ToArray();

            // Assert
            result.ShouldBe(expect);
        }

        [Theory]
        [InlineData(0, 6, 1, 0, -1, -2, -3, -4, -5)]
        [InlineData(5, 6, 1, 5, 4, 3, 2, 1, 0)]
        [InlineData(10, 6, 3, 10, 7, 4, 1, -2, -5)]
        public void Take(int from, int take, int step, params int[] expect)
        {
            // Arrange

            // Act
            var count = Count.Down(from).Take(take);
            var result = (step is 1 ? count : count.Step(step)).ToArray();

            // Assert
            result.ShouldBe(expect);
        }

        [Theory]
        [InlineData(0, 1, 0, -1, -2, -3, -4, -5)]
        [InlineData(5, 1, 5, 4, 3, 2, 1, 0)]
        [InlineData(10, 3, 10, 7, 4, 1, -2, -5)]
        public void Endless(int from, int step, params int[] expect)
        {
            // Arrange

            // Act
            var count = Count.Down(from);
            var result = (step is 1 ? count : count.Step(step)).Select(x => x).Take(expect.Length).ToArray();

            // Assert
            result.ShouldBe(expect);
        }

        [Fact]
        public void StepPreservesDownwardDirection()
        {
            // Arrange

            // Act
            var result = Count.Down(10).Step(2).To(4).ToArray();

            // Assert
            result.ShouldBe([10, 8, 6, 4]);
        }

        [Fact]
        public void NegativeStepDoesNotFlipDirection()
        {
            // Arrange

            // Act
            var positiveStep = Count.Down(10).Step(2).To(4).ToArray();
            var negativeStep = Count.Down(10).Step(-2).To(4).ToArray();

            // Assert
            negativeStep.ShouldBe(positiveStep);
            negativeStep.ShouldBe([10, 8, 6, 4]);
        }
    }

    public class TakeZero
    {
        [Fact]
        public void CountUpTakeZeroIsEmpty()
        {
            // Arrange

            // Act
            var result = Count.Up(1).Take(0).ToArray();

            // Assert
            result.ShouldBeEmpty();
        }

        [Fact]
        public void CountDownTakeZeroIsEmpty()
        {
            // Arrange

            // Act
            var result = Count.Down(5).Take(0).ToArray();

            // Assert
            result.ShouldBeEmpty();
        }

        [Fact]
        public void NegativeTakeThrows()
        {
            // Arrange

            // Act / Assert
            Should.Throw<ArgumentException>(() => Count.Up(1).Take(-1));
        }
    }

    public class Overflow
    {
        [Fact]
        public void CountingUpStopsAtIntMaxValueWithoutWrapping()
        {
            // Arrange

            // Act
            // The outer LINQ Take is a hang guard; with the fix enumeration terminates on its own.
            var result = Count.Up(int.MaxValue - 1).Take(5).Select(x => x).Take(10).ToArray();

            // Assert
            result.ShouldBe([int.MaxValue - 1, int.MaxValue]);
        }

        [Fact]
        public void CountingDownStopsAtIntMinValueWithoutWrapping()
        {
            // Arrange

            // Act
            // The outer LINQ Take is a hang guard; with the fix enumeration terminates on its own.
            var result = Count.Down(int.MinValue + 1).Take(5).Select(x => x).Take(10).ToArray();

            // Assert
            result.ShouldBe([int.MinValue + 1, int.MinValue]);
        }
    }

    public class Validation
    {
        [Fact]
        public void StepZeroThrows()
        {
            // Arrange

            // Act / Assert
            Should.Throw<ArgumentException>(() => new CountRange(0, 0, null, null));
        }

        [Fact]
        public void CountUpUnreachableTargetThrows()
        {
            // Arrange

            // Act / Assert
            Should.Throw<ArgumentException>(() => Count.Up(5).To(3));
        }

        [Fact]
        public void CountDownUnreachableTargetThrows()
        {
            // Arrange

            // Act / Assert
            Should.Throw<ArgumentException>(() => Count.Down(3).To(5));
        }

        [Fact]
        public void DoubleToThrows()
        {
            // Arrange

            // Act / Assert
            Should.Throw<ArgumentException>(() => Count.Up(0).To(5).To(9));
        }
    }

    public class ToStringFormat
    {
        [Fact]
        public void CountUpWithTarget()
        {
            // Arrange

            // Act
            var result = Count.Up(5).To(10).ToString();

            // Assert
            result.ShouldBe("Count up: 5→10");
        }

        [Fact]
        public void CountUpEndless()
        {
            // Arrange

            // Act
            var result = Count.Up(5).ToString();

            // Assert
            result.ShouldBe("Count up: 5→2147483647");
        }

        [Fact]
        public void CountDownWithTarget()
        {
            // Arrange

            // Act
            var result = Count.Down(5).To(1).ToString();

            // Assert
            result.ShouldBe("Count down: 5→1");
        }

        [Fact]
        public void CountDownEndless()
        {
            // Arrange

            // Act
            var result = Count.Down(5).ToString();

            // Assert
            result.ShouldBe("Count down: 5→-2147483648");
        }
    }

    public class Enumeration
    {
        [Fact]
        public void NonGenericEnumeratorYieldsInts()
        {
            // Arrange
            var result = new List<int>();

            // Act
            var enumerator = ((IEnumerable)Count.Up(0).To(3)).GetEnumerator();
            while (enumerator.MoveNext())
                result.Add((int)enumerator.Current!);

            // Assert
            result.ShouldBe([0, 1, 2, 3]);
        }

        [Fact]
        public void EnumerateStructIsForeachable()
        {
            // Arrange
            var result = new List<int>();

            // Act
            foreach (var i in Count.Up(0).To(3).Enumerate())
                result.Add(i);

            // Assert
            result.ShouldBe([0, 1, 2, 3]);
        }

        [Fact]
        public void ResetRestartsEnumeration()
        {
            // Arrange
            var first = new List<int>();
            var second = new List<int>();

            // Act
            var e = Count.Up(0).To(2).Enumerate();
            while (e.MoveNext())
                first.Add(e.Current);
            e.Reset();
            while (e.MoveNext())
                second.Add(e.Current);

            // Assert
            first.ShouldBe(second);
            first.ShouldBe([0, 1, 2]);
        }
    }

    public class Bounds
    {
        [Fact]
        public void CountUpToSameValueYieldsSingleElement()
        {
            // Arrange

            // Act
            var result = Count.Up(5).To(5).ToArray();

            // Assert
            result.ShouldBe([5]);
        }

        [Fact]
        public void CountDownToSameValueYieldsSingleElement()
        {
            // Arrange

            // Act
            var result = Count.Down(5).To(5).ToArray();

            // Assert
            result.ShouldBe([5]);
        }

        [Fact]
        public void TakeCutsOffBeforeTo()
        {
            // Arrange

            // Act
            var result = Count.Up(0).To(100).Take(3).ToArray();

            // Assert
            result.ShouldBe([0, 1, 2]);
        }
    }
}