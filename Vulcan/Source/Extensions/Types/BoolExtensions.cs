namespace Vulcan.Extensions;

public static class BoolExtensions
{
    extension(bool)
    {
        /// <summary>
        /// Lenient, culture-invariant parsing of common boolean words or representations.
        /// Input is trimmed and case-insensitive.
        /// </summary>
        /// <returns>The parsed value, or <see langword="null"/> if the input is empty or unknown. Never throws.</returns>
        [Pure, ContractAnnotation("null => null;notnull => canbenull")]
        public static bool? TryParse(string? value)
        {
            if (value.IsNotSet())
                return null;

            return value.Trim().ToLowerInvariant() switch
            {
                "true" or "t" or "wahr" => true,
                "false" or "f" or "falsch" => false,
                "1" => true,
                "0" => false,
                "yes" or "y" or "yeah" or "yep" or "yup" or "sure" => true,
                "ja" or "j" or "jo" or "jep" or "jup" => true,
                "oui" or "si" or "sí" or "sì" => true,
                "okay" or "ok" or "k" or "kk" => true,
                "no" or "n" or "nope" or "nop" or "nah" => false,
                "nein" or "ne" or "nee" or "nö" or "noe" => false,
                "non" => false,
                _ => null,
            };
        }

        /// <summary>C-style conversion: <c>0</c> is <see langword="false"/>, every other value is <see langword="true"/>.</summary>
        /// <remarks>Never throws, as every <see cref="int"/> is a valid input.</remarks>
        [Pure]
        public static bool Parse(int value)
            => value is not 0;
    }
}
