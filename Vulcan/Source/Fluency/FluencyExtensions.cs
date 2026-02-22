using System.Runtime.CompilerServices;
using Vulcan.Fluency.Abstraction;

namespace Vulcan.Fluency;

public static class FluencyExtensions
{
    extension<TI>(TI self)
    {
        /// <summary>Fluent Method chaining: Like Select, but for a single element.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TO Pipe<TO>(Func<TI, TO> transformer)
        {
            return transformer(self);
        }
        
        /// <inheritdoc cref="FluencyExtensions.Pipe{TI,TO}(TI,System.Func{TI,TO})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TO Pipe<TO>(IPipeable<TI, TO> pipe)
        {
            return pipe.Invoke(self);
        }

        /// <summary>
        ///     Like <see cref="FluencyExtensions.Pipe{TI,TO}(TI,System.Func{TI,TO})" />, but the return value is not propagated.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TI Call(Action<TI> action)
        {
            action(self);
            return self;
        }
        
        /// <inheritdoc cref="FluencyExtensions.Call{TI}(TI,System.Action{TI})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TI Call(ICallable<TI> action)
        {
            action.Invoke(self);
            return self;
        }
    }
}