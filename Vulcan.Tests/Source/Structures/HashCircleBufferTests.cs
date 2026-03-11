using Vulcan.Extensions;
using Vulcan.Structures;

namespace Vulcan.Tests.Structures;

[TestSubject(typeof(HashCircleBuffer<>))]
public class HashCircleBufferTests
{
    readonly HashCircleBuffer<int> _sut = new(3);

    [Fact]
    public void Empty_Contains_ReturnsFalse()
        => _sut.Contains(1).ShouldBeFalse();

    [Fact]
    public void Contains_AddedItem_ReturnsTrue()
    {
        _sut.Add(1);
        _sut.Contains(1).ShouldBeTrue();
    }

    [Fact]
    public void Contains_NotAddedItem_ReturnsFalse()
    {
        _sut.Add(1);
        _sut.Contains(2).ShouldBeFalse();
    }

    [Fact]
    public void Contains_AfterEviction_ReturnsFalse()
    {
        // Fill buffer: [1, 2, 3], then push 4 → 1 is evicted
        _sut.AddRange(1, 2, 3, 4);

        _sut.Contains(1).ShouldBeFalse();
        _sut.Contains(4).ShouldBeTrue();
    }

    [Fact]
    public void Contains_Duplicate_EvictOneKeepsOther()
    {
        // Buffer: [1, 1, 2] → add 3 → evicts first 1, second 1 still present
        _sut.AddRange(1, 1, 2);
        _sut.Add(3);

        _sut.Contains(1).ShouldBeTrue();
    }

    [Fact]
    public void Contains_Duplicate_BothEvicted_ReturnsFalse()
    {
        // Buffer: [1, 1, 2] → add 3 → add 4 → both 1s are gone
        _sut.AddRange(1, 1, 2, 3, 4);

        _sut.Contains(1).ShouldBeFalse();
    }

    [Fact]
    public void Clear_ContainsReturnsFalse()
    {
        _sut.AddRange(1, 2, 3);
        _sut.Clear();

        _sut.Contains(1).ShouldBeFalse();
        _sut.Count.ShouldBe(0);
    }

    [Fact]
    public void Enumeration_MatchesCircleBuffer()
    {
        // HashCircleBuffer should still enumerate old-to-new
        _sut.AddRange(1, 2, 3, 4);

        _sut.ToArray().ShouldBe([2, 3, 4]);
    }
}
