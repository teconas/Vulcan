using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class IntegerExtensions
{
    extension(int number)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float AsFloat()
            => number;

        ///<summary>Because in C# Modulus is wrong: (-6%2 → -6), (-6.Mod(2) → 2)</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Mod(int mod)
            => (number % mod + mod) % mod;
    }
}