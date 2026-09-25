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

    [Fact]
    public void DeferWithTarget_ExecutesActionWithTarget()
    {
        // Arrange
        var target = new object();
        object? received = null;

        // Act
        var defer = Defer(target, t => received = t);
        defer.Dispose();

        // Assert
        received.ShouldBeSameAs(target);
    }

    [Fact]
    public void DeferWithTarget_BeforeDispose_Nothing()
    {
        // Arrange
        var disposed = new LinearBool();

        // Act
        _ = Defer(disposed, t => t.Set());

        // Assert
        disposed.ShouldBeFalse();
    }

    [Fact]
    public void DeferWithTarget_DoubleDispose_ShouldBeIgnored()
    {
        // Arrange
        var disposed = 0;

        // Act
        var defer = Defer(0, _ => disposed++);
        disposed = 0;
        defer.Dispose();
        defer.Dispose();
        defer.Dispose();

        // Assert
        disposed.ShouldBe(1);
    }

    [Fact]
    public void Using_RunsOnScopeExit()
    {
        // Arrange
        var disposed = new LinearBool();

        // Act
        using (Defer(disposed.Set))
        {
            // Assert
            disposed.ShouldBeFalse();
        }

        // Assert
        disposed.ShouldBeTrue();
    }

    [Fact]
    public void StackedUsings_RunInReverseOrder()
    {
        // Arrange
        var order = new List<int>();

        // Act
        using (Defer(() => order.Add(1)))
        using (Defer(() => order.Add(2)))
        using (Defer(() => order.Add(3)))
        {
            order.ShouldBeEmpty();
        }

        // Assert
        order.ShouldBe([3, 2, 1]);
    }

    [Fact]
    public void ActionThrows_PropagatesFromDispose()
    {
        // Arrange & Act & Assert
        Should.Throw<InvalidOperationException>(
            () => Defer(() => throw new InvalidOperationException()).Dispose());
    }
}