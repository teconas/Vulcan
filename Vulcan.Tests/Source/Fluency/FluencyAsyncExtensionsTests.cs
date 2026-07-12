using Vulcan.Fluency;

namespace Vulcan.Tests.Fluency;

[TestSubject(typeof(FluencyAsyncExtensions))]
public static class FluencyAsyncExtensionsTests
{
    public class PipeAsync
    {
        [Fact]
        public async Task SyncTransformsValue()
            => (await Task.FromResult(5).PipeAsync(x => x + 1)).ShouldBe(6);

        [Fact]
        public async Task SyncChangesType()
            => (await Task.FromResult("42").PipeAsync(int.Parse)).ShouldBe(42);

        [Fact]
        public async Task SyncNullFlows()
            => (await Task.FromResult<string?>(null).PipeAsync(x => x is null)).ShouldBeTrue();

        [Fact]
        public async Task AsyncTransformsValue()
            => (await Task.FromResult(5).PipeAsync(async x => { await Task.Yield(); return x + 1; })).ShouldBe(6);

        [Fact]
        public async Task AsyncChangesType()
            => (await Task.FromResult("42").PipeAsync(async x => { await Task.Yield(); return int.Parse(x); })).ShouldBe(42);

        [Fact]
        public async Task SyncAndAsyncOverloadsGiveEqualResults()
        {
            var sync = await Task.FromResult(5).PipeAsync(x => x + 1);
            var async = await Task.FromResult(5).PipeAsync(async x => { await Task.Yield(); return x + 1; });
            sync.ShouldBe(async);
        }

        [Fact]
        public async Task AwaitsSourceBeforeTransforming()
        {
            // Arrange
            var log = new List<string>();
            var tcs = new TaskCompletionSource<int>();
            var source = tcs.Task;

            // Act
            var piped = source.PipeAsync(x =>
            {
                log.Add("transform");
                return x + 1;
            });
            log.Add("before-complete");
            tcs.SetResult(5);
            var result = await piped;

            // Assert
            result.ShouldBe(6);
            log.ShouldBe(["before-complete", "transform"]);
        }

        [Fact]
        public async Task FaultedSourcePropagatesUnwrapped()
            => await Should.ThrowAsync<InvalidOperationException>(
                async () => await Task.FromException<int>(new InvalidOperationException()).PipeAsync(x => x + 1));

        [Fact]
        public async Task SyncTransformerThrowingPropagates()
            => await Should.ThrowAsync<InvalidOperationException>(
                async () => await Task.FromResult(1).PipeAsync(int (_) => throw new InvalidOperationException()));

        [Fact]
        public async Task AsyncTransformerFaultedTaskPropagates()
            => await Should.ThrowAsync<InvalidOperationException>(
                async () => await Task.FromResult(1).PipeAsync(_ => Task.FromException<int>(new InvalidOperationException())));

        [Fact]
        public async Task FaultedSourceShortCircuitsTransformer()
        {
            // Arrange
            var ran = false;

            // Act / Assert
            await Should.ThrowAsync<InvalidOperationException>(
                async () => await Task.FromException<int>(new InvalidOperationException())
                    .PipeAsync(x =>
                    {
                        ran = true;
                        return x + 1;
                    }));

            ran.ShouldBeFalse();
        }
    }

    public class Chaining
    {
        [Fact]
        public async Task SyncThenAsyncThreadsValue()
            => (await Task.FromResult(2)
                    .PipeAsync(x => x + 3)
                    .PipeAsync(async x => { await Task.Yield(); return x * 2; }))
                .ShouldBe(10);
    }
}
