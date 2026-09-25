// ReSharper disable NotDisposedResource

using System.Diagnostics.CodeAnalysis;

namespace Vulcan.Tests.Tools;

[TestSubject(typeof(DisposableLock))]
[SuppressMessage("Usage", "xUnit1051:Calls to methods which accept CancellationToken should use TestContext.Current.CancellationToken")]
public abstract class DisposableLockTests : IDisposable
{
    static readonly TimeSpan SafeTimeout = TimeSpan.FromSeconds(5);

    readonly DisposableLock _sut = new();

    public void Dispose() => _sut.Dispose();

    public class LockSync : DisposableLockTests
    {
        [Fact]
        public void Lock_ReleasesOnDispose()
        {
            // Arrange
            var locked = _sut.Lock(SafeTimeout);

            // Act
            locked.Dispose();

            // Assert
            using var locked2 = _sut.Lock(SafeTimeout);
        }

        [Fact]
        public void Lock_BlocksSecondAcquire()
        {
            // Arrange
            using var _ = _sut.Lock(SafeTimeout);

            // Act & Assert
            Should.Throw<TimeoutException>(() => _sut.Lock(TimeSpan.FromMilliseconds(50)));
        }

        [Fact]
        public void Lock_Cancellation_ThrowsOperationCanceledException()
        {
            // Arrange
            using var _ = _sut.Lock(SafeTimeout);
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act & Assert
            Should.Throw<OperationCanceledException>(() => _sut.Lock(cts.Token));
        }
    }

    public class LockSyncWithTimeout : DisposableLockTests
    {
        [Fact]
        public void Timeout_ThrowsTimeoutException()
        {
            // Arrange
            using var _ = _sut.Lock(SafeTimeout);

            // Act & Assert
            Should.Throw<TimeoutException>(() => _sut.Lock(TimeSpan.FromMilliseconds(50)));
        }

        [Fact]
        public void Timeout_DoesNotCorruptSemaphore()
        {
            // A failed timeout must not call Release() — otherwise the semaphore count
            // would exceed its maximum of 1 and the next Release() would throw SemaphoreFullException.

            // Arrange
            var firstLock = _sut.Lock(SafeTimeout);
            Should.Throw<TimeoutException>(() => _sut.Lock(TimeSpan.FromMilliseconds(50)));

            // Act — if semaphore were corrupted, this Dispose() would throw SemaphoreFullException
            firstLock.Dispose();

            // Assert
            using var secondLock = _sut.Lock(SafeTimeout);
        }
    }

    public class LockAsyncMethods : DisposableLockTests
    {
        [Fact]
        public async Task LockAsync_ReleasesOnDispose()
        {
            // Arrange
            var locked = await _sut.LockAsync(SafeTimeout);

            // Act
            locked.Dispose();

            // Assert
            using var locked2 = await _sut.LockAsync(SafeTimeout);
        }

        [Fact]
        public async Task LockAsync_BlocksSecondAcquire()
        {
            // Arrange
            using var _ = await _sut.LockAsync(SafeTimeout);

            // Act & Assert
            await Should.ThrowAsync<TimeoutException>(() => _sut.LockAsync(TimeSpan.FromMilliseconds(50)));
        }

        [Fact]
        public async Task LockAsync_Cancellation_ThrowsOperationCanceledException()
        {
            // Arrange
            using var _ = await _sut.LockAsync(SafeTimeout);
            using var cts = new CancellationTokenSource();
            await cts.CancelAsync();

            // Act & Assert
            await Should.ThrowAsync<OperationCanceledException>(() => _sut.LockAsync(cts.Token));
        }
    }

    public class LockAsyncWithTimeout : DisposableLockTests
    {
        [Fact]
        public async Task Timeout_ThrowsTimeoutException()
        {
            // Arrange
            using var _ = await _sut.LockAsync(SafeTimeout);

            // Act & Assert
            await Should.ThrowAsync<TimeoutException>(() => _sut.LockAsync(TimeSpan.FromMilliseconds(50)));
        }

        [Fact]
        public async Task Timeout_DoesNotCorruptSemaphore()
        {
            // See LockSyncWithTimeout.Timeout_DoesNotCorruptSemaphore for explanation

            // Arrange
            var firstLock = await _sut.LockAsync(SafeTimeout);
            await Should.ThrowAsync<TimeoutException>(() => _sut.LockAsync(TimeSpan.FromMilliseconds(50)));

            // Act — if semaphore were corrupted, this Dispose() would throw SemaphoreFullException
            firstLock.Dispose();

            // Assert
            using var secondLock = await _sut.LockAsync(SafeTimeout);
        }
    }

    [SuppressMessage("Usage", "xUnit1031:Do not use blocking task operations in test method", Justification = "Blocking on the caller's context is the scenario under test")]
    public class CallerContext : DisposableLockTests
    {
        // LockAsync must not resume on the caller's SynchronizationContext: a caller blocking on
        // .Result/.Wait() from a UI/main thread would otherwise deadlock on a contended lock.

        [Fact]
        public void LockAsync_Contended_CompletesWhileCallerContextIsBlocked()
        {
            // Arrange
            var firstLock = _sut.Lock(SafeTimeout);
            using var _ = TestSynchronizationContext.Blocked().Install();
            var pending = _sut.LockAsync();

            // Act
            firstLock.Dispose();

            // Assert
            pending.Wait(SafeTimeout).ShouldBeTrue();
            pending.Result.Dispose();
        }

        [Fact]
        public void LockAsyncWithTimeout_Contended_CompletesWhileCallerContextIsBlocked()
        {
            // Arrange
            var firstLock = _sut.Lock(SafeTimeout);
            using var _ = TestSynchronizationContext.Blocked().Install();
            var pending = _sut.LockAsync(SafeTimeout);

            // Act
            firstLock.Dispose();

            // Assert
            pending.Wait(SafeTimeout).ShouldBeTrue();
            pending.Result.Dispose();
        }
    }

    public class DisposeDisposableLock : DisposableLockTests
    {
        [Fact]
        public void Dispose_CanBeCalledSafely()
        {
            // Arrange & Act & Assert
            _sut.Dispose();
        }
    }

    public class LockNoTimeout : DisposableLockTests
    {
        [Fact]
        public void Lock_NoTimeout_ReleasesOnDispose()
        {
            // Arrange
            var locked = _sut.Lock();
            locked.ShouldNotBeNull();

            // Act
            locked.Dispose();

            // Assert
            using var locked2 = _sut.Lock(SafeTimeout);
        }

        [Fact]
        public async Task LockAsync_NoTimeout_ReleasesOnDispose()
        {
            // Arrange & Act
            using (await _sut.LockAsync())
            {
            }

            // Assert
            using var locked2 = await _sut.LockAsync(SafeTimeout);
        }

        [Fact]
        public void Handle_DoubleDispose_DoesNotOverRelease()
        {
            // Arrange
            var locked = _sut.Lock();

            // Act — second Dispose must be a no-op thanks to DeferTool idempotency;
            // a non-idempotent release would throw SemaphoreFullException here.
            locked.Dispose();
            Should.NotThrow(() => locked.Dispose());

            // Assert — the lock is released exactly once, so a fresh acquire succeeds.
            using var locked2 = _sut.Lock(SafeTimeout);
        }

        [Fact]
        public void Lock_NoTimeout_MutualExclusion()
        {
            // Arrange
            using var _ = _sut.Lock();

            // Act & Assert
            Should.Throw<TimeoutException>(() => _sut.Lock(TimeSpan.FromMilliseconds(50)));
        }
    }
}
