using System.Collections;

namespace Vulcan.Structures;

public class Grouping<Key, Element>(Func<ICollection<Element>> collectionFactory) : IEnumerable<IGrouping<Key, Element>>
    where Key : notnull
{
    readonly Dictionary<Key, ICollection<Element>> _collection = [];

    public Grouping() : this(() => new List<Element>()) { }

    public void Add(Key key, Element element)
    {
        Get(key).Add(element);
    }

    public bool Remove(Key key, Element element)
    {
        if (_collection.TryGetValue(key, out var collection))
            return collection.Remove(element);

        return false;
    }

    public ICollection<Element> Get(Key key)
    {
        if (_collection.TryGetValue(key, out var collection))
            return collection;

        var newCollection = collectionFactory();
        _collection.Add(key, newCollection);
        return newCollection;
    }

    public ICollection<Element> this[Key key] => Get(key);

    public IEnumerator<IGrouping<Key, Element>> GetEnumerator()
    {
        return _collection
            .Where(kv => kv.Value.Any())
            .Select(kv => new GroupingEntry<Key, Element>(kv.Key, kv.Value) as IGrouping<Key, Element>)
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