namespace Vulcan.Extensions;

public static class BoolExtensions
{
    extension(bool)
    {
        public static bool? TryParse(string? value)
        {
            if (value.IsNotSet())
                return null;

            var trimmedValue = value.Trim().ToLower();

            return trimmedValue.Trim().ToLower() switch
            {
                "true" => true,
                "false" => false,
                "1" => true,
                "0" => false,
                "yes" or "y" => true,
                "ja" or "j" => true,
                "okay" or "ok" => true,
                "no" or "n" => false,
                "nein" or "ne" or "nö" => false,
                "nope" or "nop" => false,
                _ => bool.TryParse(trimmedValue, out var result) ? result : null,
            };
        }

        public static bool Parse(int value)
        {
            return value is not 0;
        } 
    }
}