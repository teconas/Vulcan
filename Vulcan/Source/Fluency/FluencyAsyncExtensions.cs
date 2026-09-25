namespace Vulcan.Fluency;

public static class FluencyAsyncExtensions
{
    extension<TIn>(Task<TIn> self)
    {
        /// <inheritdoc cref="FluencyExtensions.Pipe{TIn,TOut}(TIn,Func{TIn,TOut})"/>
        /// <remarks>
        /// The transformer runs on the caller's captured <see cref="SynchronizationContext"/> (e.g. Unity main thread, UI thread),
        /// so it may use context-bound APIs. Don't block on the result (<c>.Result</c>, <c>.Wait()</c>) from such a context — await it.
        /// </remarks>
        public async Task<TOut> PipeAsync<TOut>(Func<TIn, TOut> transformer)
            => transformer(await self);

        /// <inheritdoc cref="PipeAsync{TIn,TOut}(Task{TIn},Func{TIn,TOut})"/>
        public async Task<TOut> PipeAsync<TOut>(Func<TIn, Task<TOut>> transformer)
            => await transformer(await self);
    }
}
