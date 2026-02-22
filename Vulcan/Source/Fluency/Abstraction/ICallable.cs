namespace Vulcan.Fluency.Abstraction;

public interface ICallable
{
    void Invoke();
}

public interface ICallable<in T>
{
    void Invoke(T value);
}