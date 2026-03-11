using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class StringExtensions
{
    static readonly string[] LineSeparators = ["\r\n", "\n", "\r"];
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Times(this string source, int num)
    {
        return string.Concat(Enumerable.Repeat(source, num));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string[] SplitLines(this string source, StringSplitOptions options = StringSplitOptions.None)
        => source.Split(LineSeparators, options);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Times(this char source, int num)
    {
        return source.ToString().Times(num);
    }
}