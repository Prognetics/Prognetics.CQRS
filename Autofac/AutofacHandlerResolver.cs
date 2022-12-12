using Autofac;

namespace Prognetics.CQRS.Autofac;
internal class AutofacHandlerResolver : IHandlerResolver
{
    private readonly IComponentContext _context;

    public AutofacHandlerResolver(IComponentContext context)
    {
        _context = context;
    }

    public T Resolve<T>() where T : notnull
        => _context.Resolve<T>();
}
