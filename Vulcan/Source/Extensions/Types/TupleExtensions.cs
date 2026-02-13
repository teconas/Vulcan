namespace Vulcan.Extensions;

public static class TupleExtensions
{
    public static (T, T) Switch<T>(this (T, T) self)
    {
        return (self.Item2, self.Item1);
    }
}