
# Welcome to Prognetics CQRS!

We would like to share with the community our companies production verified CQRS + Mediator library.

## How it works?

Throught the Mediator object you are able to issue different kinds of handlers to be called.

First are commands:
```c#
Task SendAsync<TCommand>(TCommand command) where TCommand : ICommandAsync;
```
They allow you to run a logic that is supposed to modify the state of the application. They do not return value.

Second are queries:
```c#
TResult Fetch<TResult>(IQuery<TResult> query);

Task<TResult> FetchAsync<TResult>(IQueryAsync<TResult> query);

TResult FetchFast<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;

Task<TResult> FetchFastAsync<TQuery, TResult>(TQuery query) where TQuery : IQueryAsync<TResult>;
```
They are used to return values, and should not perform any state modification operations. In the library we have 4 ways of running them. Synchronous way, asynchronous way, and "fast" way for both of the latter. The fast way forces the definition of generic parameters, but skips the reflection mechanism used in classic fetch, which is a costly operation.


Third are events:
```c#
Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEventAsync;
```
While the queries and commands are allowed to have only single handler this is not the case for events. When a specific one is published the mediator will run all the handlers that are tied to it.

Fourth are generic commands:
```c#
Task SendGenericAsync<TCommand, TObject>(TCommand command) where TCommand : IGenericCommandAsync<TObject>;
```
They allow you to pass a command that has a generic parameter. This is not doable with regular commands.

The last ones are the generic queries:
```c#
TResult FetchGeneric<TQuery, TResult, T>(TQuery query) where TQuery : IGenericQuery<TResult, T>;
```
Similarly to the commands, they allow you to pass generic arguments in the query object.

## Usage

This library has a heavy dependency on Autofac. It needs a specific module to be registered for each DLL that you create. 
It scans the assembly looking for marking interfaces implementations.

```c#
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
```

The above is the most complex part of this solution, as it is responsible for how the objects are created.

To present the simplicity of the solution we will showcase commands. Let's say we would like to create a sum command tht will be able to add a number defined by the user to the number 2.

We will need to create two files for this. A SumCommand:
```C#
using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Tests.Integration.Command
{
    public class SumCommand : ICommandAsync
    {
        public SumCommand(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}
```
And a SumCommandHandler:

```C#
using System.Threading.Tasks;
using Prognetics.CQRS.Handlers;

namespace Prognetics.CQRS.Tests.Integration.Command
{
    public class SumCommandHandler : CommandHandlerAsync<SumCommand>
    {
        public override async Task HandleAsync(SumCommand command)
        {
            var result = 2 + command.Number;
        }
    }
}
```
That's it! Now in order to launch the handler you need to make a following call through the mediator:
```C#
await mediator.SendAsync(new SumCommand(2));
```


You can find all the examples of different operations in the Tests project.


## Running Tests

The tests project is based on xunit with visual studio runner. To run right-click the project file and select "Run Tests".

