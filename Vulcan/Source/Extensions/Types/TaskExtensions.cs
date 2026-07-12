namespace Vulcan.Extensions;

public static class TaskExtensions
{
    extension(Task)
    {
        /// <summary>
        /// Asynchronously waits until the specified point in time is reached.
        /// </summary>
        /// <remarks>
        /// The target is converted to UTC and compared against <see cref="DateTime.UtcNow"/>,
        /// so daylight saving transitions between now and the target are accounted for.
        /// <see cref="DateTimeKind.Local"/> and <see cref="DateTimeKind.Unspecified"/> targets
        /// are treated as local time.
        /// Returns immediately when the target time is already in the past.
        /// </remarks>
        /// <param name="dateTime">The point in time to wait for.</param>
        /// <param name="ct">A token to cancel the wait.</param>
        public static async Task DelayUntil(DateTime dateTime, CancellationToken ct = default)
        {
            var delay = dateTime.ToUniversalTime() - DateTime.UtcNow;
            if (delay <= TimeSpan.Zero)
                return;

            await Task.Delay(delay, ct).ConfigureAwait(false);
        }
    }
}