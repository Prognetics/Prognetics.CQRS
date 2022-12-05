using System;

namespace Prognetics.CQRS.Simplified;

public interface IHandlerResolver
{
    T Resolve<T>();
    object Resolve(Type t);
}
