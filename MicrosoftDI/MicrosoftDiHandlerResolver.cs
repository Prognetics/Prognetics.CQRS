using Microsoft.Extensions.DependencyInjection;

namespace Prognetics.CQRS.MicrosoftDI;
public class MicrosoftDiHandlerResolver : IHandlerResolver
{
    private readonly IServiceProvider _serviceProvider;

    public MicrosoftDiHandlerResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T Resolve<T>() where T : notnull
        => _serviceProvider.GetRequiredService<T>();
}
