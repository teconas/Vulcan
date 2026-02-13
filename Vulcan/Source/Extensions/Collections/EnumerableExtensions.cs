using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class EnumerableExtensions
{
    public static ICollection<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        var collection = source.ToArray();
        foreach (var item in collection)
            action(item);

        return collection;
    }

    /// <summary>Flattening an IEnumerable of IEnumerables</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> source)
        => source.SelectMany(x => x);

    /// <summary>Distinct elements by selector</summary>
    public static IEnumerable<T> Distinct<T, TS>(this IEnumerable<T> source, Func<T, TS> selector)
    {
        var seenKeys = new HashSet<TS>();
        foreach (var element in source)
            if (seenKeys.Add(selector(element)))
                yield return element;
    }

    /// <summary>Remove all null values from an IEnumerable</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> SkipNull<T>(this IEnumerable<T?>? source)
        => source?.OfType<T>() ?? [];
}