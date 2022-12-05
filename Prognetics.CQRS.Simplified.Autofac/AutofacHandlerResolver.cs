using Autofac;
using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Tests.Simplified.Shared;
internal class AutofacHandlerResolver : IHandlerResolver
{
    private readonly IComponentContext _context;

    public AutofacHandlerResolver(IComponentContext context)
    {
        _context = context;
    }

    public T Resolve<T>() where T: notnull
        => _context.Resolve<T>();

    public object Resolve(Type t)
        => _context.Resolve(t);
}
