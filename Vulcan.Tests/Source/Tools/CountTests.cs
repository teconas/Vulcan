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
    }
}