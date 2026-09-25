namespace Vulcan.Tests.Helper;

/// <summary>Simulates a single-threaded host context (Unity main thread, WPF/WinForms UI thread) in async tests.</summary>
public sealed class TestSynchronizationContext : SynchronizationContext
{
    readonly bool _runPosts;

    TestSynchronizationContext(bool runPosts) => _runPosts = runPosts;

    /// <summary>A context whose thread is blocked: posted continuations never run — like a main thread stuck in <c>.Result</c>.</summary>
    public static TestSynchronizationContext Blocked() => new(false);

    /// <summary>A context that runs posted continuations on the thread pool with itself installed as <see cref="SynchronizationContext.Current"/>.</summary>
    public static TestSynchronizationContext Running() => new(true);

    public override void Post(SendOrPostCallback callback, object? state)
    {
        if (_runPosts is false)
            return;

        ThreadPool.QueueUserWorkItem(_ => RunInContext(callback, state));
    }

    /// <summary>Installs this context on the current thread; disposing restores the previous one.</summary>
    [MustDisposeResource]
    public IDisposable Install()
    {
        var previous = Current;
        SetSynchronizationContext(this);
        return DeferTool.Defer(() => SetSynchronizationContext(previous));
    }

    void RunInContext(SendOrPostCallback callback, object? state)
    {
        using var _ = Install();
        callback(state);
    }
}
