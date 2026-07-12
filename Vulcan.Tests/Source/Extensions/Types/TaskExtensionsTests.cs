using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types;

[TestSubject(typeof(Vulcan.Extensions.TaskExtensions))]
public static class TaskExtensionsTests
{
    public class DelayUntil
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);

        [Fact]
        public async Task PastLocalTime_CompletesImmediately()
        {
            // Arrange
            var target = DateTime.Now.AddSeconds(-5);

            // Act
            var task = Task.DelayUntil(target, TestContext.Current.CancellationToken);
            var winner = await Task.WhenAny(task, Task.Delay(Timeout, TestContext.Current.CancellationToken));

            // Assert
            winner.ShouldBe(task);
            task.IsCompletedSuccessfully.ShouldBeTrue();
        }

        [Fact]
        public async Task PastUtcTime_CompletesImmediately()
        {
            // Arrange — regression guard: with the old DateTime.Now comparison,
            // a UTC time 5s in the past could yield a positive delay on machines
            // with a positive UTC offset.
            var target = DateTime.UtcNow.AddSeconds(-5);
            target.Kind.ShouldBe(DateTimeKind.Utc);

            // Act
            var task = Task.DelayUntil(target, TestContext.Current.CancellationToken);
            var winner = await Task.WhenAny(task, Task.Delay(Timeout, TestContext.Current.CancellationToken));

            // Assert
            winner.ShouldBe(task);
            task.IsCompletedSuccessfully.ShouldBeTrue();
        }

        [Fact]
        public async Task NearFutureLocalTime_CompletesAfterShortDelay()
        {
            // Arrange
            var target = DateTime.Now.AddMilliseconds(200);

            // Act
            var task = Task.DelayUntil(target, TestContext.Current.CancellationToken);
            var winner = await Task.WhenAny(task, Task.Delay(Timeout, TestContext.Current.CancellationToken));

            // Assert
            winner.ShouldBe(task);
            task.IsCompletedSuccessfully.ShouldBeTrue();
        }

        [Fact]
        public async Task NearFutureUtcTime_CompletesAfterShortDelay()
        {
            // Arrange
            var target = DateTime.UtcNow.AddMilliseconds(200);

            // Act
            var task = Task.DelayUntil(target, TestContext.Current.CancellationToken);
            var winner = await Task.WhenAny(task, Task.Delay(Timeout, TestContext.Current.CancellationToken));

            // Assert
            winner.ShouldBe(task);
            task.IsCompletedSuccessfully.ShouldBeTrue();
        }
    }
}
