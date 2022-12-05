using System;

namespace Prognetics.CQRS;

public interface IHandlerResolver
{
    T Resolve<T>();
    object Resolve(Type t);
}
