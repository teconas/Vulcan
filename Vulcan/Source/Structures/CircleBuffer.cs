using System.Collections;
using Vulcan.Extensions;

namespace Vulcan.Structures;

/// <summary>Circular Buffer with fixed size. Access Order is old to new.</summary>
public class CircleBuffer<T>(int capacity) : ICollection<T>
{
    readonly T[] _buffer = new T[capacity];
    int _head = 0;

    public int Count { get; private set; }
    
    public bool IsReadOnly => false;

    public void Add(T item)
    {
        _buffer[_head++] = item;
        Count = Math.Max(_head, Count);
        _head %= capacity;
    }

    public bool Remove(T item) => throw new NotSupportedException();

    public void Clear()
    {
        _head = 0;
        Count = 0;
    }
    
    public bool Contains(T item)
        => ToEnumerable().Contains(item);

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array.Length - arrayIndex > Count)
            throw new ArgumentException("Destination array is not large enough.");
        if(arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        for (var i=0; i<Count;i++)
            array[i + arrayIndex] = this[i];
    }

    /// <summary>
    /// 0 is oldest, n is newest.
    /// n+1 is oldest again.
    /// -1 is newest, -n is oldest again.
    /// </summary>
    public T this[int index] => _buffer[(_head + index).Mod(Count)];

    public IEnumerator<T> GetEnumerator() => ToEnumerable().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    IEnumerable<T> ToEnumerable()
    {
        if (Count < capacity)
            return _buffer.Take(Count);
        
        return _buffer.Concat(_buffer).Skip(_head).Take(Count);
    }
}