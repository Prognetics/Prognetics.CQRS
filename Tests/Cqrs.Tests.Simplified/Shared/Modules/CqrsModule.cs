using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Prognetics.CQRS.Simplified.Tests.Shared;
using Module = Autofac.Module;

namespace Prognetics.CQRS.Simplified.Tests.Shared.Modules
{
    public class CqrsModule : Module
    {
        private readonly string _assemblyName;

        public CqrsModule(string assemblyName)
        {
            _assemblyName = assemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assembly = Assembly.Load(_assemblyName);

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
                            .InstancePerDependency();
                    }
                    else
                    {
                        builder.RegisterType(objectToRegister)
                            .AsImplementedInterfaces()
                            .InstancePerDependency();
                    }
                }
            }

            ScanAssemblyAndRegister(typeof(ICommandHandler<>));
            ScanAssemblyAndRegister(typeof(IQueryHandler<,>));
            ScanAssemblyAndRegister(typeof(IEventHandler<>));

            builder.RegisterType<AutofacHandlerResolver>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<Mediator>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}