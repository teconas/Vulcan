namespace Vulcan.Fluency;

public static class FluencyAsyncExtensions
{
    extension<TIn>(Task<TIn> self)
    {
        /// <inheritdoc cref="FluencyExtensions.Pipe{TI,TO}(TI,Func{TI,TO})"/>
        public async Task<TOut> PipeAsync<TOut>(Func<TIn, TOut> transformer)
            => transformer(await self);

        /// <inheritdoc cref="FluencyExtensions.Pipe{TI,TO}(TI,Func{TI,TO})"/>
        public async Task<TOut> PipeAsync<TOut>(Func<TIn, Task<TOut>> transformer)
            => await transformer(await self);
    }
}