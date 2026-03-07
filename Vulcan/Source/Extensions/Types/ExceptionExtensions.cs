using System.Reflection;

namespace Vulcan.Extensions;

public static class ExceptionExtensions
{
    extension(Exception ex)
    {
        /// <summary> Get the Full Message of all nested messages. </summary>
        /// <example>"OuterExceptionMessage: InnerExceptionMessage"</example>
        /// <remarks>
        /// - Exceptions that add nothing to the message will be skipped.
        /// (e.g. AggregatedException, TargetInvocationException) <br/>
        /// - Does only include Messages not the stack.
        /// </remarks>
        public string FullMessage()
        {
            var messages = CollectMessages(ex).ToList();
            return messages
                .Select((msg, i) => i < messages.Count - 1 ? StripTrailingPunctuation(msg) : msg)
                .Join(": ");
        }
    }

    static IEnumerable<string> CollectMessages(Exception? ex) => ex switch
    {
        null => [],
        _ when TryUnwrap(ex) is { } unwrapped => CollectMessages(unwrapped),
        AggregateException { InnerExceptions.Count: > 1 } => [ex.Message],
        _ => [ex.Message, .. CollectMessages(ex.InnerException)],
    };

    static Exception? TryUnwrap(Exception ex) => ex switch
    {
        AggregateException { InnerExceptions: [var single] } => single,
        TargetInvocationException { InnerException: { } inner } => inner,
        TypeInitializationException { InnerException: { } inner } => inner,
        _ => null,
    };

    static string StripTrailingPunctuation(string message) =>
        message.Length > 0 && message[^1] is '.' or '!' or '?' or ';' or ':'
            ? message[..^1]
            : message;
}
