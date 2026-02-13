using System.Runtime.CompilerServices;

namespace Vulcan.Fluency;

public static class TransformExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TransformIf<T>(this T self, Func<T, bool> condition, Func<T, T> transformator)
    {
        return condition(self) ? transformator(self) : self;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TransformIf<T>(this T self, Func<bool> condition, Func<T, T> transformator)
    {
        return condition() ? transformator(self) : self;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TransformIf<T>(this T self, bool condition, Func<T, T> transformator)
    {
        return condition ? transformator(self) : self;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TransformIf<T>(this T self, Func<T, bool> condition, Func<T> transformator)
    {
        return condition(self) ? transformator() : self;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TransformIf<T>(this T self, Func<bool> condition, Func<T> transformator)
    {
        return condition() ? transformator() : self;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TransformIf<T>(this T self, bool condition, Func<T> transformator)
    {
        return condition ? transformator() : self;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TransformIf<T>(this T self, Func<T, bool> condition, T value)
    {
        return condition(self) ? value : self;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TransformIf<T>(this T self, Func<bool> condition, T value)
    {
        return condition() ? value : self;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T TransformIf<T>(this T self, bool condition, T value)
    {
        return condition ? value : self;
    }
}