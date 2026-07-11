using System.Collections;
using System.Runtime.CompilerServices;
using Vulcan.Extensions;
using Vulcan.Structures;

namespace Vulcan.Tests.Structures;

[TestSubject(typeof(CircleBuffer<>))]
public class CircleBufferTests
{
    readonly CircleBuffer<int> _sut = new(3);

    [Fact]
    public void Empty_Count()
        => _sut.Count.ShouldBe(0);

    [Fact]
    public void Empty_Value()
        => Enumerate(_sut).ShouldBe([]);
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void CountValue(int elements)
    {
        // Arrange
        _sut.AddRange(Count.Up(0).To(elements));
        
        // Act
        var result = _sut.Count;
        
        // Assert
        const int capacity = 3;
        result.ShouldBe(Math.Min(capacity, elements + 1));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public void Adding(int elements)
    {
        // Arrange
        _sut.AddRange(Count.Up(1).To(elements));
        
        // Act
        
        // Assert
        var expect = Count.Up(1).To(elements).TakeLast(3).ToArray();
        Enumerate(_sut).ShouldBe(expect);
    }

    [Theory]
    [InlineData(-9,2)]
    [InlineData(-1,2)]
    [InlineData(0,1)]
    [InlineData(1,2)]
    [InlineData(2,1)]
    [InlineData(3,2)]
    [InlineData(4,1)]
    [InlineData(9,2)]
    public void Indexing_BeforeCapacity(int index, int expected)
    {
        // Arrange
        _sut.AddRange(1,2);
        
        // Act
        var result = _sut[index];
        
        // Assert
        result.ShouldBe(expected);
    }
    
    [Theory]
    [InlineData(-9,2)]
    [InlineData(-1,4)]
    [InlineData(0,2)]
    [InlineData(1,3)]
    [InlineData(2,4)]
    [InlineData(3,2)]
    [InlineData(4,3)]
    [InlineData(9,2)]
    public void Indexing_AfterCapacity(int index, int expected)
    {
        // Arrange
        _sut.AddRange(1, 2, 3, 4);
        
        // Act
        var result = _sut[index];
        
        // Assert
        result.ShouldBe(expected);
    }

    [Fact]
    public void CopyTo_CopiesOldestToNewest()
    {
        // Arrange
        _sut.AddRange(1, 2, 3, 4, 5);
        var array = new int[4];
        // Act
        _sut.CopyTo(array, 1);

        // Assert
        array.ShouldBe([0,3,4,5]);
    }

    [Fact]
    public void CopyTo_DestinationTooSmall_Throws()
    {
        // Arrange
        _sut.AddRange(1, 2, 3);
        var array = new int[2];

        // Act / Assert
        Should.Throw<ArgumentException>(() => _sut.CopyTo(array, 0));
    }

    [Fact]
    public void CopyTo_NegativeIndex_Throws()
    {
        // Arrange
        _sut.AddRange(1, 2, 3);
        var array = new int[3];

        // Act / Assert
        Should.Throw<ArgumentOutOfRangeException>(() => _sut.CopyTo(array, -1));
    }

    [Fact]
    public void ZeroCapacity_Throws()
        => Should.Throw<ArgumentOutOfRangeException>(() => new CircleBuffer<int>(0));

    [Fact]
    public void NegativeCapacity_Throws()
        => Should.Throw<ArgumentOutOfRangeException>(() => new CircleBuffer<int>(-1));

    [Fact]
    public void CapacityOfOne_Works()
    {
        // Arrange
        var buffer = new CircleBuffer<int>(1);

        // Act
        buffer.Add(42);

        // Assert
        buffer.Count.ShouldBe(1);
        buffer[0].ShouldBe(42);
    }

    [Fact]
    public void Clear_ResetsCountAndEnumeration()
    {
        // Arrange
        _sut.AddRange(1, 2, 3, 4, 5);

        // Act
        _sut.Clear();

        // Assert
        _sut.Count.ShouldBe(0);
        Enumerate(_sut).ShouldBe([]);
    }

    [Fact]
    public void Clear_ThenReAdd_EnumeratesCorrectly()
    {
        // Arrange
        _sut.AddRange(1, 2, 3, 4, 5);
        _sut.Clear();

        // Act
        _sut.AddRange(6, 7);

        // Assert
        _sut.Count.ShouldBe(2);
        _sut.Select(x => x).ShouldBe([6, 7]);
    }

    [Fact]
    public void Clear_ReleasesReferences()
    {
        // Arrange
        var buffer = new CircleBuffer<object>(3);
        var weak = AddThrowawayReference(buffer);

        // Act
        buffer.Clear();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // Assert
        weak.IsAlive.ShouldBeFalse();
    }

    [Fact]
    public void Capacity_ReturnsCtorValue()
    {
        // Arrange
        var buffer = new CircleBuffer<int>(3);

        // Act / Assert
        buffer.Capacity.ShouldBe(3);

        // Overfilling does not change the capacity
        buffer.AddRange(1, 2, 3, 4, 5);
        buffer.Capacity.ShouldBe(3);
    }

    [Fact]
    public void IsReadOnly_IsFalse()
        => _sut.IsReadOnly.ShouldBeFalse();

    [Fact]
    public void Remove_ThrowsNotSupported()
        => Should.Throw<NotSupportedException>(() => _sut.Remove(1));

    [Fact]
    public void Contains_PresentElement_ReturnsTrue()
    {
        // Arrange
        _sut.AddRange(1, 2, 3);

        // Act / Assert
        _sut.Contains(2).ShouldBeTrue();
    }

    [Fact]
    public void Contains_AbsentElement_ReturnsFalse()
    {
        // Arrange
        _sut.AddRange(1, 2, 3);

        // Act / Assert
        _sut.Contains(99).ShouldBeFalse();
    }

    [Fact]
    public void Contains_EmptyBuffer_ReturnsFalse()
        => _sut.Contains(1).ShouldBeFalse();

    [Fact]
    public void Contains_EvictedElement_ReturnsFalse()
    {
        // Arrange
        _sut.AddRange(1, 2, 3, 4);

        // Act / Assert
        _sut.Contains(1).ShouldBeFalse();
        _sut.Contains(4).ShouldBeTrue();
    }

    [Fact]
    public void GetEnumerator_NonGeneric_EnumeratesOldestToNewest()
    {
        // Arrange
        _sut.AddRange(1, 2, 3, 4, 5);

        // Act
        var result = new List<int>();
        var enumerator = ((IEnumerable)_sut).GetEnumerator();
        while (enumerator.MoveNext())
            result.Add((int)enumerator.Current!);

        // Assert
        result.ShouldBe([3, 4, 5]);
    }

    [Theory]
    [InlineData(2, false)]
    [InlineData(3, true)]
    [InlineData(5, true)]
    public void IsAtCapacity(int elements, bool expected)
    {
        // Arrange
        _sut.AddRange(Count.Up(1).To(elements));

        // Act / Assert
        _sut.IsAtCapacity.ShouldBe(expected);
    }

    [Fact]
    public void Indexer_OnEmptyBuffer_ThrowsArgumentOutOfRange()
        => Should.Throw<ArgumentOutOfRangeException>(() => { var _ = _sut[0]; });

    // Kept in its own method so the added object has no lingering strong reference on the stack.
    [MethodImpl(MethodImplOptions.NoInlining)]
    static WeakReference AddThrowawayReference(CircleBuffer<object> buffer)
    {
        var item = new object();
        buffer.Add(item);
        return new WeakReference(item);
    }

    static IEnumerable<int> Enumerate(CircleBuffer<int> buffer)
        => buffer.Select(x => x);
}