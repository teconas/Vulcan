namespace Vulcan.Fluency;

public static class FluencyAsyncExtensions
{
    extension<TIn>(Task<TIn> self)
    {
        /// <summary>
        ///     Monad like Bind for Tasks
        /// </summary>
        public async Task<TOut> PipeAsync<TOut>(Func<TIn, TOut> transformer)
            => transformer(await self);

        /// <summary>
        ///     Monad like Bind for Tasks
        /// </summary>
        public async Task<TOut> PipeAsync<TOut>(Func<TIn, Task<TOut>> transformer)
            => await transformer(await self);
    }
}