using System.Threading.Tasks;
using Autofac;
using Prognetics.CQRS.Mediator;
using Prognetics.CQRS.Tests.Shared.GenericCommand;
using Prognetics.CQRS.Tests.Shared.Modules;
using Xunit;

namespace Prognetics.CQRS.Tests.Integration.GenericCommand
{
    public class GenericCommandTests
    {
        private readonly IContainer _container;

        public GenericCommandTests()
        {
            _container = BuildContainer();
        }

        [Fact]
        public async Task GenericCommandShouldBeFoundAndExecuted()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var command = new TestGenericCommand<SimpleData>(new SimpleData("Hello!"));
                await mediator.SendGenericAsync<TestGenericCommand<SimpleData>, SimpleData>(command);
            }
        }

        [Fact]
        public async Task ShouldExecuteTwoGenericCommandHandlersWithDifferentTypesPassed()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var command = new TestGenericCommand<SimpleData>(new SimpleData("Hello!"));
                await mediator.SendGenericAsync<TestGenericCommand<SimpleData>, SimpleData>(command);

                var otherCommand = new TestGenericCommand<SomeEntity>(new SomeEntity("SomeEntity Hello!"));
                await mediator.SendGenericAsync<TestGenericCommand<SomeEntity>, SomeEntity>(otherCommand);
            }
        }

        private IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new CqrsModule("Prognetics.Cqrs.Tests"));
            return containerBuilder.Build();
        }
    }
}