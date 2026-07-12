using Vulcan.Fluency;

namespace Vulcan.Tests.Fluency;

[TestSubject(typeof(FluencyExtensions))]
public static class FluencyExtensionsTests
{
    public class Pipe
    {
        [Fact]
        public void TransformsValue()
            => 5.Pipe(x => x + 1).ShouldBe(6);

        [Fact]
        public void ChangesType()
            => "42".Pipe(int.Parse).ShouldBe(42);

        [Fact]
        public void IdentityReturnsSame()
            => 7.Pipe(x => x).ShouldBe(7);

        [Fact]
        public void WorksWithReferenceType()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3 };

            // Act
            var count = list.Pipe(x => x.Count);

            // Assert
            count.ShouldBe(3);
        }

        [Fact]
        public void TransformerReturningNullReturnsNull()
            => "x".Pipe(_ => (string?)null).ShouldBeNull();

        [Fact]
        public void NullInputHandledCleanly()
            => ((string?)null).Pipe(x => x is null).ShouldBeTrue();

        [Fact]
        public void ExceptionPropagatesUnwrapped()
            => Should.Throw<InvalidOperationException>(
                () => 1.Pipe(int (_) => throw new InvalidOperationException()));
    }

    public class Call
    {
        [Fact]
        public void ExecutesAction()
        {
            // Arrange
            var called = false;

            // Act
            5.Call(_ => called = true);

            // Assert
            called.ShouldBeTrue();
        }

        [Fact]
        public void ReceivesActualSelf()
        {
            // Arrange
            var seen = 0;

            // Act
            42.Call(x => seen = x);

            // Assert
            seen.ShouldBe(42);
        }

        [Fact]
        public void ReturnsSameReference()
        {
            // Arrange
            var list = new List<int>();

            // Act
            var result = list.Call(x => x.Add(1));

            // Assert
            result.ShouldBeSameAs(list);
        }

        [Fact]
        public void ReturnsEqualValue()
            => 9.Call(_ => { }).ShouldBe(9);

        [Fact]
        public void ExceptionPropagates()
            => Should.Throw<InvalidOperationException>(
                () => 1.Call(_ => throw new InvalidOperationException()));

        [Fact]
        public void ResultUsableForChaining()
            => 3.Call(_ => { }).Pipe(x => x * 2).ShouldBe(6);
    }

    public class Chaining
    {
        [Fact]
        public void PipeThenPipeThreadsValue()
            => 2.Pipe(x => x + 3).Pipe(x => x * 2).ShouldBe(10);

        [Fact]
        public void MultipleCallsRunInOrder()
        {
            // Arrange
            var log = new List<int>();

            // Act
            1.Call(_ => log.Add(1)).Call(_ => log.Add(2)).Call(_ => log.Add(3));

            // Assert
            log.ShouldBe([1, 2, 3]);
        }

        [Fact]
        public void InterleavedCallAndPipe()
        {
            // Arrange
            var log = new List<string>();

            // Act
            var result = 4
                .Call(x => log.Add($"in:{x}"))
                .Pipe(x => x * 2)
                .Call(x => log.Add($"out:{x}"));

            // Assert
            result.ShouldBe(8);
            log.ShouldBe(["in:4", "out:8"]);
        }
    }
}
