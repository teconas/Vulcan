#if NETSTANDARD2_0
namespace System;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
readonly struct Index : IEquatable<Index>
{
    // Positive: from start. Negative (bitwise complement): from end.
    readonly int _value;

    public Index(int value, bool fromEnd = false)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be non-negative.");
        _value = fromEnd ? ~value : value;
    }

    public static Index Start => new(0);
    public static Index End   => new(~0);

    public static Index FromStart(int value) => new(value);
    public static Index FromEnd(int value)   => new(value, fromEnd: true);

    public bool IsFromEnd => _value < 0;
    public int  Value     => _value < 0 ? ~_value : _value;

    public int GetOffset(int length) => IsFromEnd ? length - Value : Value;

    public static implicit operator Index(int value) => FromStart(value);

    public bool Equals(Index other)         => _value == other._value;
    public override bool Equals(object? obj) => obj is Index other && Equals(other);
    public override int GetHashCode()        => _value;
    public override string ToString()        => IsFromEnd ? $"^{Value}" : Value.ToString();
}

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
readonly struct Range(Index start, Index end) : IEquatable<Range>
{
    public Index Start { get; } = start;
    public Index End   { get; } = end;

    public static Range All                    => new(Index.Start, Index.End);
    public static Range StartAt(Index start)   => new(start, Index.End);
    public static Range EndAt(Index end)       => new(Index.Start, end);

    public (int Offset, int Length) GetOffsetAndLength(int length)
    {
        var start = Start.GetOffset(length);
        var end   = End.GetOffset(length);
        if ((uint)end > (uint)length || (uint)start > (uint)end)
            throw new ArgumentOutOfRangeException(nameof(length));
        return (start, end - start);
    }

    public bool Equals(Range other)          => Start.Equals(other.Start) && End.Equals(other.End);
    public override bool Equals(object? obj)  => obj is Range other && Equals(other);
    public override int GetHashCode()         => (Start.GetHashCode() * 397) ^ End.GetHashCode();
    public override string ToString()         => $"{Start}..{End}";
}
#endif
