using Microsoft.Extensions.DependencyInjection;

namespace Prognetics.CQRS.MicrosoftDI;
public class MicrosoftDIHandlerResolver : IHandlerResolver
{
    private readonly IServiceProvider _serviceProvider;

    public MicrosoftDIHandlerResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T Resolve<T>() where T : notnull
        => _serviceProvider.GetRequiredService<T>();
}
