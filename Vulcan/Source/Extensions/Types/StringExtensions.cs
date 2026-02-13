using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Vulcan.Extensions;

public static class StringExtensions
{
    extension(string source)
    {
        [StringFormatMethod("source")]
        public string Format(params object[] args)
        {
            return string.Format(source, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Times(int num)
        {
            return string.Concat(Enumerable.Repeat(source, num));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Times(this char source, int num)
    {
        return source.ToString().Times(num);
    }

    extension(string? value)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string? EmptyToNull()
        {
            return value is { Length: > 0 } ? value : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string? EmptyOrWhitespaceToNull()
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [ContractAnnotation("value:null => false")]
    public static bool IsSet([NotNullWhen(true)] this string? value)
        => string.IsNullOrWhiteSpace(value) is false;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [ContractAnnotation("value:null => true")]
    public static bool IsNotSet([NotNullWhen(false)] this string? value)
        => string.IsNullOrWhiteSpace(value);
}