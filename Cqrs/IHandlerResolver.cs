namespace Prognetics.CQRS;

public interface IHandlerResolver
{
    T Resolve<T>();
}
