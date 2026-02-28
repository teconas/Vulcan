namespace Vulcan.Tests.Helper;

public static class AllocationChecker
{
    public static long CountAllocBytes(Action action, int warmup = 3, int runs = 10)
    {
        CleanupGarbageCollector();

        for (var i = 0; i < warmup; i++)
            action();

        CleanupGarbageCollector();

        var before = GC.GetAllocatedBytesForCurrentThread();

        for (var i = 0; i < runs; i++)
            action();

        var after = GC.GetAllocatedBytesForCurrentThread();
        return after - before;
    }

    static void CleanupGarbageCollector()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
}