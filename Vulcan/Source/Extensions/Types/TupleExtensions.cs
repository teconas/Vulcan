namespace Vulcan.Extensions;

public static class TupleExtensions
{
    public static (T, T) Swap<T>(this (T, T) self)
        => (self.Item2, self.Item1);
}