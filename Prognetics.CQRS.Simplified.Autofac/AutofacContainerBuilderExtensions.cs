using Autofac;
using Autofac.Core.Registration;
using System.Reflection;

namespace Prognetics.CQRS.Simplified.Autofac;
public static class AutofacContainerBuilderExtensions
{
    public static IModuleRegistrar RegisterProgenticsCQRSModule(
        this ContainerBuilder containerBuilder,
        params Assembly[] assemblies)
        => containerBuilder.RegisterModule(new CqrsModule(assemblies.ToList()));
}
