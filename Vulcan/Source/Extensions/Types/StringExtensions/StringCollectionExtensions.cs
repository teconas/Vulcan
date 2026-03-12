using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class StringCollectionExtensions
{
    extension(IEnumerable<string?> source)
    {
        /// <summary>Removes entries from the source where the string is null or empty.</summary>
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<string> WhereSet() => (IEnumerable<string>)source.WhereNot(string.IsNullOrEmpty);

        /// <summary>Removes entries from the source where the string is null or empty or whitespace.</summary>
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<string> WhereTrimmedSet() => (IEnumerable<string>)source.WhereNot(string.IsNullOrWhiteSpace);

        /// <summary>Trims each string element, and filters out any that are null or empty after trimming</summary>
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<string> TrimAndFilter()
            => source.Select(x => x?.Trim()).WhereSet();

        /// <summary>Trims each string element using the specified characters, and filters out any that are null or empty after trimming</summary>
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<string> TrimAndFilter(params char[] trimChars)
            => source.Select(x => x?.Trim(trimChars)).WhereSet();
    }
}