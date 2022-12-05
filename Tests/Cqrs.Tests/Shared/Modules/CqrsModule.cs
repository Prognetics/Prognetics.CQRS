using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Prognetics.CQRS.Handlers;
using Prognetics.CQRS.Markers;
using Module = Autofac.Module;

namespace Prognetics.CQRS.Tests.Shared.Modules
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

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<ICommandHandlerAsync>())
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IQueryHandler>())
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IQueryHandlerAsync>())
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IEventHandlerAsync>())
                .AsImplementedInterfaces()
                .InstancePerDependency();

            ScanAssemblyAndRegister(assembly, typeof(IGenericCommandHandlerAsync<>), builder);

            ScanAssemblyAndRegister(assembly, typeof(IGenericQueryHandler<,,>), builder);

            builder.Register<Func<Type, ICommandHandlerAsync>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return t =>
                {
                    var handlerType = typeof(ICommandHandlerAsync<>).MakeGenericType(t);
                    return (ICommandHandlerAsync)ctx.Resolve(handlerType);
                };
            });

            builder.Register<Func<Type, object>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return t =>
                {
                    var typeDeterminingInterface = t.GetInterfaces().Single();

                    if (typeDeterminingInterface.Name == typeof(IGenericCommandAsync<>).Name)
                    {
                        var handlerType = typeof(IGenericCommandHandlerAsync<>).MakeGenericType(t);
                        return ctx.Resolve(handlerType);
                    }
                    else if (typeDeterminingInterface.Name == typeof(IGenericQuery<,>).Name)
                    {

                        var handlerType = typeof(IGenericQueryHandler<,,>).MakeGenericType(t, typeDeterminingInterface.GenericTypeArguments[0], typeDeterminingInterface.GenericTypeArguments[1]);
                        return ctx.Resolve(handlerType);
                    }

                    throw new ArgumentException($"Wrong type passed to generic command/query handler resolver {t}");
                };
            });

            builder.Register<Func<Type, IQueryHandler>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                var queryType = typeof(IQuery<>);

                return t =>
                {
                    var handlerType = typeof(IQueryHandler<,>).MakeGenericType(t, t.GetInterfaces().Single(x => x.Name == queryType.Name).GenericTypeArguments[0]);
                    return (IQueryHandler)ctx.Resolve(handlerType);
                };
            });

            builder.Register<Func<Type, IQueryHandlerAsync>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                var queryType = typeof(IQueryAsync<>);

                return t =>
                {
                    var handlerType = typeof(IQueryHandlerAsync<,>).MakeGenericType(t, t.GetInterfaces().Single(x => x.Name == queryType.Name).GenericTypeArguments[0]);
                    return (IQueryHandlerAsync)ctx.Resolve(handlerType);
                };
            });

            builder.Register<Func<Type, IEnumerable<IEventHandlerAsync>>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                return t =>
                {
                    var handlerType = typeof(IEventHandlerAsync<>).MakeGenericType(t);
                    var handlersCollectionType = typeof(IEnumerable<>).MakeGenericType(handlerType);
                    return (IEnumerable<IEventHandlerAsync>)ctx.Resolve(handlersCollectionType);
                };
            });

            builder.RegisterType<Mediator.Mediator>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private void ScanAssemblyAndRegister(Assembly assembly, Type type, ContainerBuilder builder)
        {
            var objectsToRegister = assembly.GetTypes().Where(t =>
            {
                return t.GetTypeInfo()
                    .ImplementedInterfaces.Any(
                        i => i.IsGenericType && i.GetGenericTypeDefinition() == type);
            });

            foreach (var objectToRegister in objectsToRegister)
            {
                builder.RegisterGeneric(objectToRegister)
                    .AsImplementedInterfaces()
                    .InstancePerDependency();
            }
        }
    }
}