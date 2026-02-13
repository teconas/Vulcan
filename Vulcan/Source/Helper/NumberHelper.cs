using Vulcan.Extensions;

namespace Vulcan;

public static class NumberHelper
{
    public static bool? TryParseBool(string? value)
    {
        if (value.IsSet() is false)
            return null;

        return value.ToLower() switch
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
            _ => null
        };
    }

    public static bool ParseBool(int value)
    {
        return value is not 0;
    }
}