using System.Collections;

namespace Vulcan.Structures;

public class Grouping<TKey, TElement>(Func<ICollection<TElement>> collectionFactory) : IEnumerable<IGrouping<TKey, TElement>>
    where TKey : notnull
{
    readonly Dictionary<TKey, ICollection<TElement>> _collection = [];

    public Grouping() : this(() => new List<TElement>()) { }

    public void Add(TKey key, TElement element)
    {
        Get(key).Add(element);
    }

    public bool Remove(TKey key, TElement element)
    {
        if (_collection.TryGetValue(key, out var collection))
            return collection.Remove(element);

        return false;
    }

    /// <summary>Return the collection for key, or create and store an empty one (get-or-add). Empty groups are skipped on enumeration.</summary>
    public ICollection<TElement> Get(TKey key)
    {
        if (_collection.TryGetValue(key, out var collection))
            return collection;

        var newCollection = collectionFactory();
        _collection.Add(key, newCollection);
        return newCollection;
    }

    /// <inheritdoc cref="Get"/>
    public ICollection<TElement> this[TKey key] => Get(key);

    public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
    {
        return _collection
            .Where(kv => kv.Value.Any())
            .Select(kv => new GroupingEntry<TKey, TElement>(kv.Key, kv.Value) as IGrouping<TKey, TElement>)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

file readonly record struct GroupingEntry<TKey, TElement>(TKey Key, ICollection<TElement> Entry)
    : IGrouping<TKey, TElement>
{
    public IEnumerator<TElement> GetEnumerator() => Entry.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => Entry.GetEnumerator();
}