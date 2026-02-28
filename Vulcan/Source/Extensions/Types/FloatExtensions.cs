using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class FloatExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float? NanToNull(this float? value)
        => value is null or float.NaN ? null : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float? NanToNull(this float value)
        => float.IsNaN(value) ? null : value;

    ///<summary>Because in C# Modulus is wrong: (-6%2 → -6), (-6.Mod(2) → 2)</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Mod(this float self, float mod)
        => (self % mod + mod) % mod;
    
    ///<summary>Because in C# Modulus is wrong: (-6%2 → -6), (-6.Mod(2) → 2)</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Mod(this float self, int mod)
        => (self % mod + mod) % mod;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(this float number, float min, float max)
    {
        if (max < min || float.IsNaN(number) || float.IsNaN(min) || float.IsNaN(max))
            throw new ArgumentException("Clamping required max >= min & all numbers to be non-NaN");
        
        if (number < min)
            return min;
        if (number > max)
            return max;
        return number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp01(this float number)
    {
        if (number >= 1)
            return 1;
        if (number <= 0)
            return 0;
        return number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximately(this float self, float other, float precision = float.Epsilon * 8)
        => Math.Abs(self - other) <= precision;
}

// Float Fluency
public static partial class DoubleExtensions
{
    /// <summary>Rounds a number to the given precision.</summary>
    /// <remark>
    /// Result will be a whole numbered factor of <paramref name="precision"/>
    /// </remark>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float RoundTo(this float number, float precision)
        => (float)Math.Round(number / precision) * precision;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float RoundTo(this float number)
        => (float)Math.Round(number);

    /// <inheritdoc cref="RoundTo(float,float)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RoundToInt(this float number)
        => (int)Math.Round(number);

    /// <summary>Floors a number to the given precision.</summary>
    /// <remark>
    /// Result will be a whole numbered factor of <paramref name="precision"/>
    /// </remark>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FloorTo(this float number, float precision)
        => (float)Math.Floor(number / precision) * precision;
    
    /// <inheritdoc cref="FloorTo(float,float)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FloorTo(this float number)
        => (float)Math.Floor(number);
    
    /// <inheritdoc cref="FloorTo(float,float)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FloorToInt(this float number)
        => (int)Math.Floor(number);

    /// <summary>Ceils a number to the given precision.</summary>
    /// <remark>
    /// Result will be a whole numbered factor of <paramref name="precision"/>
    /// </remark>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float CeilTo(this float number, float precision)
        => (float)Math.Ceiling(number / precision) * precision;
    
    /// <inheritdoc cref="CeilTo(float,float)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float CeilTo(this float number)
        => (float)Math.Ceiling(number);
    
    /// <inheritdoc cref="CeilTo(float,float)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CeilToInt(this float number)
        => (int)Math.Ceiling(number);
}