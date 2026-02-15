using static Vulcan.DeferTool;

namespace Vulcan.Tests.Tools;

[TestSubject(typeof(DeferTool))]
public class DeferToolTests
{
    [Fact]
    public void Dispose_ExecutesAction()
    {
        // Arrange
        var disposed = false;
        
        // Act
        var defer = Defer(()=> disposed = true);
        defer.Dispose();
        
        // Assert
        disposed.ShouldBeTrue();
    }

    [Fact]
    public void BeforeDispose_Nothing()
    {
        // Arrange
        var disposed = false;
        
        // Act
        _ = Defer(()=> disposed = true);
        
        // Assert
        disposed.ShouldBeFalse();
    }
    
    [Fact]
    public void DoubleDispose_ShouldBeIgnored()
    {
        // Arrange
        var disposed = 0;
        
        // Act
        var defer = Defer(()=> disposed++);
        disposed = 0;
        defer.Dispose();
        defer.Dispose();
        defer.Dispose();
        
        // Assert
        disposed.ShouldBe(1);
    }
}