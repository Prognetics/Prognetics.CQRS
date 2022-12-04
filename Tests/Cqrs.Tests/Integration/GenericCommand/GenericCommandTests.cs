using System.Linq;
using System.Threading.Tasks;
using Autofac;
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
        public void Test()
        {
            var isRegistered = _container.ComponentRegistry.Registrations.Any();

            Assert.True(isRegistered);
        }

        [Fact]
        public async Task GenericCommandShouldBeFoundAndExecuted()
        {
            using var scope = _container.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            var command = new TestGenericCommand<SimpleData>(new SimpleData("Hello!"));
            await mediator.Send(command);
        }

        [Fact]
        public async Task ShouldExecuteTwoGenericCommandHandlersWithDifferentTypesPassed()
        {
            using var scope = _container.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            var command = new TestGenericCommand<SimpleData>(new SimpleData("Hello!"));
            await mediator.Send(command);

            var otherCommand = new TestGenericCommand<SomeEntity>(new SomeEntity("SomeEntity Hello!"));
            await mediator.Send(otherCommand);
        }

        private IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new CqrsModule("Prognetics.Cqrs.Tests"));
            return containerBuilder.Build();
        }
    }
}