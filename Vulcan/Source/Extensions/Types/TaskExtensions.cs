namespace Vulcan.Extensions;

public static class TaskExtensions
{
    extension(Task)
    {
        public static async Task DelayUntil(DateTime dateTime, CancellationToken ct = default)
        {
            var delay = dateTime - DateTime.Now;
            if (delay <= TimeSpan.Zero)
                return;

            await Task.Delay(delay, ct);
        }
    }
}