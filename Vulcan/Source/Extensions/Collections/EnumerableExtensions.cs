using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class EnumerableExtensions
{
    /// <summary>Opposite of <see cref="Enumerable.Where{T}(IEnumerable{T}, Func{T, bool})"/></summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => source.Where(x => !predicate(x));
    
    /// <summary>Opposite of <see cref="Enumerable.Where{T}(IEnumerable{T}, Func{T, int, bool})"/></summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
        => source.Where((x,i) => !predicate(x,i));
    
    /// <summary>Executes <paramref name="action"/> eagerly for every element and returns the materialized elements.</summary>
    /// <remarks>An <see cref="ICollection{T}"/> source is iterated and returned as-is; any other source is copied into a new array.
    /// Note: on <see cref="List{T}"/> the instance method <c>List&lt;T&gt;.ForEach</c> takes precedence.</remarks>
    public static ICollection<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        var collection = source as ICollection<T> ?? source.ToArray();
        foreach (var item in collection)
            action(item);

        return collection;
    }

    /// <summary>Flattening an IEnumerable of IEnumerables</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> source)
        => source.SelectMany(x => x);

#if !NET6_0_OR_GREATER
    /// <summary>Distinct elements by selector; the first occurrence wins.</summary>
    /// <remarks>Only available below .NET 6 — use LINQ's <c>DistinctBy</c> on newer targets.
    /// Intentionally not named <c>DistinctBy</c> to avoid ambiguity with LINQ for consumers of the netstandard2.0 build.</remarks>
    public static IEnumerable<T> Distinct<T, TS>(this IEnumerable<T> source, Func<T, TS> selector)
    {
        var seenKeys = new HashSet<TS>();
        foreach (var element in source)
            if (seenKeys.Add(selector(element)))
                yield return element;
    }
#endif

    /// <summary>Remove all null values from an IEnumerable</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?>? source)
        => source?.OfType<T>() ?? [];

    /// <summary>The opposite of .Any()</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None<T>(this IEnumerable<T> source)
        => !source.Any();
    
    /// <summary>The opposite of .Any()</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None<T>(this IEnumerable<T> source,  Func<T, bool> predicate)
        => !source.Any(predicate);
}