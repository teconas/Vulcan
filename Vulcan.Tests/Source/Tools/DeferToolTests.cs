using static Vulcan.DeferTool;
// ReSharper disable AccessToModifiedClosure
// ReSharper disable NotDisposedResource

namespace Vulcan.Tests.Tools;

[TestSubject(typeof(DeferTool))]
public class DeferToolTests
{
    [Fact]
    public void Dispose_ExecutesAction()
    {
        // Arrange
        var disposed = new LinearBool();
        
        // Act
        var defer = Defer(disposed.Set);
        defer.Dispose();
        
        // Assert
        disposed.ShouldBeTrue();
    }

    [Fact]
    public void BeforeDispose_Nothing()
    {
        // Arrange
        var disposed = new LinearBool();
        
        // Act
        _ = Defer(disposed.Set);
        
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