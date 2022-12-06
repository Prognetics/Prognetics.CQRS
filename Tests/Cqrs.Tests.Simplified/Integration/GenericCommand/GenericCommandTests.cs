using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Prognetics.CQRS.Simplified;
using Prognetics.CQRS.Tests.Simplified.Shared.GenericCommand;
using Xunit;

namespace Prognetics.CQRS.Tests.Simplified.Integration.GenericCommand
{
    public class GenericCommandTests : SimplifiedTestsBase
    {
        [Fact]
        public async Task GenericCommandShouldBeFoundAndExecuted()
        {
            using var scope = Container.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            var command = new TestGenericCommand<SimpleData>(new SimpleData("Hello!"));
            await mediator.SendAsync(command);
        }

        [Fact]
        public async Task ShouldExecuteTwoGenericCommandHandlersWithDifferentTypesPassed()
        {
            using var scope = Container.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            var command = new TestGenericCommand<SimpleData>(new SimpleData("Hello!"));
            await mediator.SendAsync(command);

            var otherCommand = new TestGenericCommand<SomeEntity>(new SomeEntity("SomeEntity Hello!"));
            await mediator.SendAsync(otherCommand);
        }
    }
}