using System.Reflection;
using Autofac;
using Prognetics.CQRS.Tests.Simplified.Shared;
using Module = Autofac.Module;

namespace Prognetics.CQRS.Simplified.Autofac;

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
            void ScanAssemblyAndRegister(Type type)
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

            ScanAssemblyAndRegister(typeof(ICommandHandler<>));
            ScanAssemblyAndRegister(typeof(IAsyncCommandHandler<>));
            ScanAssemblyAndRegister(typeof(IQueryHandler<,>));
            ScanAssemblyAndRegister(typeof(IAsyncQueryHandler<,>));
            ScanAssemblyAndRegister(typeof(IEventHandler<>));
        }

        builder.RegisterType<AutofacHandlerResolver>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterType<Mediator>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}