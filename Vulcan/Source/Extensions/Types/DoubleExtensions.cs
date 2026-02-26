using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static partial class DoubleExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double? NanToNull(this double? value)
        => value is null or double.NaN ? null : value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double? NanToNull(this double value)
        => double.IsNaN(value) ? null : value;

    ///<summary>Because in C# Modulus is wrong: (-6%2 → -6), (-6.Mod(2) → 2)</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Mod(this double self, double mod)
        => (self % mod + mod) % mod;
    
    ///<summary>Because in C# Modulus is wrong: (-6%2 → -6), (-6.Mod(2) → 2)</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Mod(this double self, int mod)
        => (self % mod + mod) % mod;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamp(this double number, double min, double max)
    {
        if (max < min || double.IsNaN(number) || double.IsNaN(min) || double.IsNaN(max))
            throw new ArgumentException("Clamping required max >= min & all numbers to be non-NaN");
        
        if (number < min)
            return min;
        if (number > max)
            return max;
        return number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamp01(this double number)
    {
        if (number >= 1)
            return 1;
        if (number <= 0)
            return 0;
        return number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximately(this double self, double other, double precision = float.Epsilon * 8)
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
    public static double RoundTo(this double number, double precision)
        => Math.Round(number / precision) * precision;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double RoundTo(this double number)
        => Math.Round(number);

    /// <inheritdoc cref="RoundTo(double,double)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RoundToInt(this double number)
        => (int)Math.Round(number);

    /// <summary>Floors a number to the given precision.</summary>
    /// <remark>
    /// Result will be a whole numbered factor of <paramref name="precision"/>
    /// </remark>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double FloorTo(this double number, double precision)
        => Math.Floor(number / precision) * precision;
    
    /// <inheritdoc cref="FloorTo(double,double)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double FloorTo(this double number)
        => Math.Floor(number);
    
    /// <inheritdoc cref="FloorTo(double,double)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FloorToInt(this double number)
        => (int)Math.Floor(number);

    /// <summary>Ceils a number to the given precision.</summary>
    /// <remark>
    /// Result will be a whole numbered factor of <paramref name="precision"/>
    /// </remark>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CeilTo(this double number, double precision)
        => Math.Ceiling(number / precision) * precision;
    
    /// <inheritdoc cref="CeilTo(double,double)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CeilTo(this double number)
        => Math.Ceiling(number);
    
    /// <inheritdoc cref="CeilTo(double,double)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CeilToInt(this double number)
        => (int)Math.Ceiling(number);
}