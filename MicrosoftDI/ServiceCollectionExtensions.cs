using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Prognetics.CQRS.MicrosoftDI;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProgneticsCQRS(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            ScanAssemblyAndRegister(typeof(ICommandHandler<>), services, assembly);
            ScanAssemblyAndRegister(typeof(IAsyncCommandHandler<>), services, assembly);
            ScanAssemblyAndRegister(typeof(IQueryHandler<,>), services, assembly);
            ScanAssemblyAndRegister(typeof(IAsyncQueryHandler<,>), services, assembly);
            ScanAssemblyAndRegister(typeof(IEventHandler<>), services, assembly);
        }

        services.AddScoped<IHandlerResolver, MicrosoftDIHandlerResolver>();
        services.AddScoped<IMediator, Mediator>();

        return services;
    }

    private static void ScanAssemblyAndRegister(
        Type type,
        IServiceCollection services,
        Assembly assembly)
    {
        var objectsToRegister = assembly.GetTypes().Where(t =>
            t.GetTypeInfo()
                .ImplementedInterfaces.Any(
                    i => i.IsGenericType && i.GetGenericTypeDefinition() == type));

        foreach (var objectToRegister in objectsToRegister)
        {
            if (objectToRegister.IsGenericType)
            {
                continue;
            }

            var serviceTypes = objectToRegister.GetTypeInfo().ImplementedInterfaces
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == type);

            foreach (var serviceType in serviceTypes)
            {
                services.AddScoped(serviceType, objectToRegister);
            }
        }
    }
}
