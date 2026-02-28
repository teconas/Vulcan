using JetBrains.Annotations;

namespace Vulcan;

public static class DeferTool
{
    /// <summary>
    /// Similar to golang's "defer" keyword.
    /// Usage: <example><code>using defered = DeferTool.Defer(()=>DoThisOnDispose);</code></example>
    /// </summary>
    /// <remarks>Hint: Use With Static Global using</remarks>
    [MustDisposeResource]
    public static IDisposable Defer(Action action)
        => new DeferredAction(action);
    
    /// <inheritdoc cref="Defer(Action)"/>
    [MustDisposeResource]
    public static IDisposable Defer<T>(T target, Action<T> action)
        => new DeferredAction(()=>action.Invoke(target));
}

file record struct DeferredAction(Action action) : IDisposable
{
    bool _disposed = false;
    public void Dispose()
    {
        if (_disposed)
            return;
        
        _disposed = true;
        action();
    }
}