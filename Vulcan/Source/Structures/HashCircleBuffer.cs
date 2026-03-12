using Vulcan.Extensions;

namespace Vulcan.Structures;

/// <summary>
/// Does the same as <see cref="CircleBuffer{T}"/>,
/// but also keeps a HashSet for a faster <see cref="Contains"/>
/// </summary>
public class HashCircleBuffer<T>(int capacity) : CircleBuffer<T>(capacity)
    where T : notnull
{
    readonly Dictionary<T, int> _counts = [];

    public override void Add(T item)
    {
        if (IsAtCapacity)
            Decrement(this[0]);

        base.Add(item);
        _counts[item] = _counts.GetOrDefault(item) + 1;
    }

    public override bool Contains(T item)
        => _counts.ContainsKey(item);

    public override void Clear()
    {
        base.Clear();
        _counts.Clear();
    }

    void Decrement(T item)
    {
        if (_counts[item] <= 1)
            _counts.Remove(item);
        else
            _counts[item]--;
    }
}