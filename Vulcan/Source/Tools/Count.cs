using System.Collections;

namespace Vulcan;

///<summary>Helper class similar to Enumerable.Range, but better.</summary>
public static class Count
{
    /// <summary>
    ///     Count up from (including) the start Number. Use <see cref="CountRange.To" /> or <see cref="CountRange.Take" />
    ///     for a upper limit.
    /// </summary>
    public static CountRange Up(int from)
    {
        return new CountRange(1, from, null, null);
    }

    /// <summary>
    ///     Count down from (including) the start Number. Will not stop at zero. Use <see cref="CountRange.To" /> or
    ///     <see cref="CountRange.Take" />for a lower limit.
    /// </summary>
    public static CountRange Down(int from)
    {
        return new CountRange(-1, from, null, null);
    }
}

/// <summary>Current Counting Info. For alloc free Enumeration call <see cref="Enumerate" /></summary>
public readonly struct CountRange : IEnumerable<int>
{
    readonly int _step;
    readonly int _from;
    readonly int? _to;
    readonly int? _take;

    public CountRange(int step, int from, int? to, int? take)
    {
        if (step == 0)
            throw new ArgumentException("Step must be non-zero.");
        _step = step;

        _from = from;

        if (to.HasValue && ((to.Value < from && step > 0) || (to.Value > from && step < 0)))
            throw new ArgumentException(
                $"Target value {to} is not reachable counting {(step > 0 ? "up" : "down")} from {from}");

        if (take is <= 0)
            throw new ArgumentException($"Take value {take} must be positive.");

        _to = to;
        _take = take;
    }

    ///<summary>Set a target number to count up/down to.</summary>
    public CountRange To(int to)
    {
        if (_to.HasValue)
            throw new ArgumentException($"{ToString()} already has a end value: {_to}");

        return new CountRange(_step, _from, to, _take);
    }

    /// <summary>Instead of an upper limit a target number of elements is counted. Will respect <see cref="Step" /></summary>
    public CountRange Take(int take)
    {
        return new CountRange(_step, _from, _to, take);
    }

    ///<summary>Step a step size for counting. If target number is not on step, it will stop the number before.</summary>
    public CountRange Step(int step)
    {
        return new CountRange(_step > 0 ? step : -step, _from, _to, _take);
    }

    public override string ToString()
    {
        return $"Count {(_step > 0 ? "up" : "down")}: {_from}→{_to ?? (_step > 0 ? int.MaxValue : int.MinValue)}";
    }

    public CountRangeEnumerator Enumerate()
    {
        return new CountRangeEnumerator(this);
    }

    public IEnumerator<int> GetEnumerator()
    {
        return Enumerate();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Enumerate();
    }

    public struct CountRangeEnumerator : IEnumerator<int>
    {
        readonly CountRange _countRange;
        readonly bool _up;
        readonly int _end;
        int _index;

        public int Current { get; private set; }

        public CountRangeEnumerator(CountRange countRange)
        {
            _countRange = countRange;
            _up = _countRange._step > 0;
            _end = _countRange._to ?? (_up ? int.MaxValue : int.MinValue);
            _index = -1;
            Current = _countRange._from;
        }

        public bool MoveNext()
        {
            _index++;
            if (_index is 0 && _countRange._take is not 0)
                return true;

            Current += _countRange._step;

            if (_index >= _countRange._take)
                return false;

            if ((_up && Current > _end) || (!_up && Current < _end))
                return false;

            return true;
        }

        public void Reset()
        {
            _index = -1;
            Current = _countRange._from;
        }

        public CountRangeEnumerator GetEnumerator()
        {
            return this;
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}