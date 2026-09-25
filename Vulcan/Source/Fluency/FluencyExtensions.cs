using System.Runtime.CompilerServices;

namespace Vulcan.Fluency;

public static class FluencyExtensions
{
    extension<TIn>(TIn self)
    {
        /// <summary>Fluent Method chaining: Like Select, but for a single element.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TOut Pipe<TOut>(Func<TIn, TOut> transformer)
        {
            return transformer(self);
        }

        /// <summary>
        ///     Like <see cref="FluencyExtensions.Pipe{TIn,TOut}(TIn,System.Func{TIn,TOut})" />, but the return value is not propagated.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TIn Call(Action<TIn> action)
        {
            action(self);
            return self;
        }
    }
}