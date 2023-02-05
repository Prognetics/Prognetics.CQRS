using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Prognetics.CQRS.Autofac;

internal class CqrsModule : Module
{
    private readonly IReadOnlyList<Assembly> _assemblies;

    public CqrsModule(IReadOnlyList<Assembly> assemblies)
    {
        _assemblies = assemblies;
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        foreach (var assembly in _assemblies)
        {
            ScanAssemblyAndRegister(typeof(ICommandHandler<>), builder, assembly);
            ScanAssemblyAndRegister(typeof(IAsyncCommandHandler<>), builder, assembly);
            ScanAssemblyAndRegister(typeof(IQueryHandler<,>), builder, assembly);
            ScanAssemblyAndRegister(typeof(IAsyncQueryHandler<,>), builder, assembly);
            ScanAssemblyAndRegister(typeof(IEventHandler<>), builder, assembly);
        }

        builder.RegisterType<AutofacHandlerResolver>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterType<Mediator>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }

    private static void ScanAssemblyAndRegister(
        Type type,
        ContainerBuilder builder,
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
                builder.RegisterGeneric(objectToRegister)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType(objectToRegister)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }
        }
    }
}