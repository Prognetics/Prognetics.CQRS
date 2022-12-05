using System.Threading.Tasks;
using Autofac;
using Prognetics.CQRS.Simplified.Tests.Shared.Modules;
using Xunit;

namespace Prognetics.CQRS.Simplified.Tests.Integration.Command
{
    public class CommandTests
    {
        private readonly IContainer _container;

        public CommandTests()
        {
            _container = BuildContainer();
        }

        [Fact]
        public async Task CommandShouldBeFoundAndExecuted()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                await mediator.Send(new SumCommand(2));
            }
        }

        private IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new CqrsModule("Prognetics.CQRS.Simplified.Tests"));
            return containerBuilder.Build();
        }
    }
}