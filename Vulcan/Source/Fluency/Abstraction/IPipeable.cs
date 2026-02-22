namespace Vulcan.Fluency.Abstraction;

public interface IPipeable<TIn, TOut>
{
    TOut Invoke(TIn input);
}