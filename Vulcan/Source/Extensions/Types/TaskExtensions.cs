namespace Vulcan.Extensions;

public static class TaskExtensions
{
    extension(Task)
    {
        /// <summary>
        /// Asynchronously waits until the specified point in time is reached.
        /// </summary>
        /// <remarks>
        /// The reference clock is chosen to match the target's <see cref="DateTimeKind"/>:
        /// a <see cref="DateTimeKind.Utc"/> target is compared against <see cref="DateTime.UtcNow"/>,
        /// while <see cref="DateTimeKind.Local"/> and <see cref="DateTimeKind.Unspecified"/> targets
        /// are compared against <see cref="DateTime.Now"/> (Unspecified is treated as local).
        /// Returns immediately when the target time is already in the past.
        /// </remarks>
        /// <param name="dateTime">The point in time to wait for.</param>
        /// <param name="ct">A token to cancel the wait.</param>
        public static async Task DelayUntil(DateTime dateTime, CancellationToken ct = default)
        {
            var now = dateTime.Kind == DateTimeKind.Utc ? DateTime.UtcNow : DateTime.Now;
            var delay = dateTime - now;
            if (delay <= TimeSpan.Zero)
                return;

            await Task.Delay(delay, ct).ConfigureAwait(false);
        }
    }
}