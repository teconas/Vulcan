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
        result.ShouldBe(Math.Min(5, _sut.Count));
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
    public void ToArray()
    {
        // Arrange
        _sut.AddRange(1, 2, 3, 4, 5);
        var array = new int[4];
        // Act
        _sut.CopyTo(array, 1);
        
        // Assert
        array.ShouldBe([0,3,4,5]);
    }

    static IEnumerable<int> Enumerate(CircleBuffer<int> buffer)
        => buffer.Select(x => x);
}