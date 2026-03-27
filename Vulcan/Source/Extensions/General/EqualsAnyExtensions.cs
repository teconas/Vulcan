using System.Runtime.CompilerServices;

// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable LoopCanBeConvertedToQuery

namespace Vulcan.Extensions;

/// <summary>Equality-comparison extensions for any type.</summary>
public static class EqualsAnyTypedExtensions
{
    extension<T>(T value)
    {
        // ── 1 argument ────────────────────────────────────────────────────────

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T other)
            => value.EqualsAny(other, EqualityComparer<T>.Default);

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T other, IEqualityComparer<T> comparer)
            => comparer.Equals(value, other);

        // ── 2 arguments ───────────────────────────────────────────────────────

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T a, T b)
            => value.EqualsAny(a, b, EqualityComparer<T>.Default);

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T a, T b, IEqualityComparer<T> comparer)
            => comparer.Equals(value, a) || comparer.Equals(value, b);

        // ── 3 arguments ───────────────────────────────────────────────────────

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T a, T b, T c)
            => value.EqualsAny(a, b, c, EqualityComparer<T>.Default);

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T a, T b, T c, IEqualityComparer<T> comparer)
            => comparer.Equals(value, a) || comparer.Equals(value, b) || comparer.Equals(value, c);

        // ── 4 arguments ───────────────────────────────────────────────────────

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T a, T b, T c, T d)
            => value.EqualsAny(a, b, c, d, EqualityComparer<T>.Default);

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T a, T b, T c, T d, IEqualityComparer<T> comparer)
            => comparer.Equals(value, a) || comparer.Equals(value, b)
            || comparer.Equals(value, c) || comparer.Equals(value, d);

        // ── 5 arguments ───────────────────────────────────────────────────────

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T a, T b, T c, T d, T e)
            => value.EqualsAny(a, b, c, d, e, EqualityComparer<T>.Default);

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualsAny(T a, T b, T c, T d, T e, IEqualityComparer<T> comparer)
            => comparer.Equals(value, a) || comparer.Equals(value, b) || comparer.Equals(value, c)
            || comparer.Equals(value, d) || comparer.Equals(value, e);

        // ── array fallback (6+ arguments) ─────────────────────────────────────

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        public bool EqualsAny(params T[] others)
            => value.EqualsAny(others, EqualityComparer<T>.Default);

        /// <summary>Returns <see langword="true"/> if <c>value</c> equals any of the given candidates.</summary>
        public bool EqualsAny(T[] others, IEqualityComparer<T> comparer)
        {
            for (var i = 0; i < others.Length; i++)
            {
                if (comparer.Equals(value, others[i]))
                    return true;
            }

            return false;
        }
    }
}
