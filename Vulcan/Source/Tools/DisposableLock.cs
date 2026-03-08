namespace Vulcan;

/// <summary>A Lock based on a <see cref="SemaphoreSlim"/>, that can be used with <see langword="using"/>.</summary>
/// <example><code>
/// readonly DisposableLock _lock = new();
/// ...
/// {
///     // Lock is acquired with Semaphore
///     using var locked = _lock.Lock();
///     ...
///     // Scope ends, and because of the using, the lock is released automatically
/// }
/// </code></example>
public interface IDisposableLock
{
    [MustDisposeResource]
    Task<IDisposable> LockAsync(CancellationToken token = default);
    
    [MustDisposeResource]
    Task<IDisposable> LockAsync(TimeSpan timeout, CancellationToken token = default);
    
    [MustDisposeResource]
    IDisposable Lock(CancellationToken token = default);
    
    [MustDisposeResource]
    IDisposable Lock(TimeSpan timeout, CancellationToken token = default);
}

/// <inheritdoc cref="IDisposableLock"/>
public class DisposableLock : IDisposableLock, IDisposable
{
    readonly SemaphoreSlim _semaphore = new(1, 1);

    [MustDisposeResource]
    public async Task<IDisposable> LockAsync(CancellationToken token = default)
    {
        await _semaphore.WaitAsync(token);
        return DeferTool.Defer(() => _semaphore.Release());
    }

    [MustDisposeResource]
    public async Task<IDisposable> LockAsync(TimeSpan timeout, CancellationToken token = default)
    {
        if (await _semaphore.WaitAsync(timeout, token) is false)
            throw new TimeoutException("Could not acquire lock within the specified timeout.");
        return DeferTool.Defer(() => _semaphore.Release());
    }

    [MustDisposeResource]
    public IDisposable Lock(CancellationToken token = default)
    {
        _semaphore.Wait(token);
        return DeferTool.Defer(() => _semaphore.Release());
    }

    [MustDisposeResource]
    public IDisposable Lock(TimeSpan timeout, CancellationToken token = default)
    {
        if (_semaphore.Wait(timeout, token) is false)
            throw new TimeoutException("Could not acquire lock within the specified timeout.");
        return DeferTool.Defer(() => _semaphore.Release());
    }

    public void Dispose() => _semaphore.Dispose();
}