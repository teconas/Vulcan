using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Vulcan.Extensions;

public static class StringFormatExtensions
{
    extension(string source)
    {
        /// <inheritdoc cref="Format(string,object?[])"/>
        [StringFormatMethod("source"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(object? arg0)
            => string.Format(source, arg0);

        /// <inheritdoc cref="Format(string,object?[])"/>
        [StringFormatMethod("source"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(object? arg0, object? arg1)
            => string.Format(source, arg0, arg1);

        /// <inheritdoc cref="Format(string,object?[])"/>
        [StringFormatMethod("source"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(object? arg0, object? arg1, object? arg2)
            => string.Format(source, arg0, arg1, arg2);

        /// <inheritdoc cref="Format(string,object?[])"/>
        [StringFormatMethod("source"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(object? arg0, object? arg1, object? arg2, object? arg3)
            => string.Format(source, arg0, arg1, arg2, arg3);

        /// <summary>Fluent version of <see cref="string.Format(string,object[])"/></summary>
        [StringFormatMethod("source"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Format(params object?[] args)
            => string.Format(source, args);
    }
}