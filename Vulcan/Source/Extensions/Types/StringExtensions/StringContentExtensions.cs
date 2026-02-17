using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Vulcan.Extensions;

public static class StringContentExtensions
{
    /// <summary>A string is 'set' if it is not null and not empty. Whitespace does NOT count as empty.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [ContractAnnotation("value:null => false")]
    public static bool IsSet([NotNullWhen(true)] this string? value)
        => string.IsNullOrWhiteSpace(value) is false;

    /// <summary>A string is 'set' if it is not null and not empty. Whitespace does NOT count as empty.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [ContractAnnotation("value:null => true")]
    public static bool IsNotSet([NotNullWhen(false)] this string? value)
        => string.IsNullOrWhiteSpace(value);
    
    extension(string? value)
    {
        /// <summary>
        /// Converts empty strings to a null value. Whitespaces is NOT considered empty. See: <see cref="EmptyOrWhitespaceToNull"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("value:null => null; value:notnull => canbenull")]
        public string? EmptyToNull()
        {
            return value is { Length: > 0 } ? value : null;
        }

        /// <summary>
        /// Converts empty or whitespace-only strings to a null value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("value:null => null; value:notnull => canbenull")]
        public string? EmptyOrWhitespaceToNull()
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
    }
}