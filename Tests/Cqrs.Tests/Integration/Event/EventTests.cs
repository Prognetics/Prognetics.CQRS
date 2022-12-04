using System.Threading.Tasks;
using Autofac;
using Prognetics.CQRS.Mediator;
using Prognetics.CQRS.Tests.Shared.Modules;
using Xunit;

namespace Prognetics.CQRS.Tests.Integration.Event
{
    public class EventTests
    {
        private readonly IContainer _container;

        public EventTests()
        {
            _container = BuildContainer();
        }

        [Fact]
        public async Task FireEvent()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                await mediator.PublishAsync(new NumberEvent(2));
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