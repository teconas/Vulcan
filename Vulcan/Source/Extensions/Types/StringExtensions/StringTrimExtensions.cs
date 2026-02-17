namespace Vulcan.Extensions;

public static class StringTrimExtensions
{
    extension(string source)
    {

        /// <summary>
        /// Trims an entire string (or multiple instances) from the beginning of another string
        /// </summary>
        /// <returns>Trimmed string</returns>
        public string TrimStart(string prefix, StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (prefix.IsNotSet())
                return source;

            var start = 0;
            while (start + prefix.Length <= source.Length &&
                   string.Compare(source, start, prefix, 0, prefix.Length, comparisonType) == 0)
                start += prefix.Length;

            return start == 0 ? source : source.Substring(start);
        }

        /// <summary>
        /// Trims an entire string (or multiple instances) from the end of another string
        /// </summary>
        /// <returns>Trimmed string</returns>
        public string TrimEnd(string suffix, StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (suffix.IsNotSet())
                return source;

            var end = source.Length;
            while (end - suffix.Length >= 0 &&
                   string.Compare(source, end - suffix.Length, suffix, 0, suffix.Length, comparisonType) == 0)
                end -= suffix.Length;

            return end == source.Length ? source : source.Substring(0, end);
        }

        /// <summary>
        /// Trims an entire string (or multiple instances) from the beginning and end of another string
        /// </summary>
        /// <returns>Trimmed string</returns>
        public string Trim(string token, StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (token.IsNotSet())
                return source;

            var start = 0;
            var end = source.Length;

            while (start + token.Length <= end && string.Compare(source, start, token, 0, token.Length, comparisonType) == 0)
                start += token.Length;

            while (end - token.Length >= start && string.Compare(source, end - token.Length, token, 0, token.Length, comparisonType) == 0)
                end -= token.Length;

            return start == 0 && end == source.Length ? source : source.Substring(start, end - start);
        }
    }
}