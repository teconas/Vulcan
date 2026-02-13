using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class JoinExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// Fluent <see cref="string.Join(string, object[])"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Join(string separator)
            => string.Join(separator, source);

        /// <summary>
        /// Fluent <see cref="string.Join(string, object[])"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Join(char separator)
#if NET10_0
            => string.Join(separator, source);
#else
            => string.Join(separator.ToString(), source);
#endif
    }
}