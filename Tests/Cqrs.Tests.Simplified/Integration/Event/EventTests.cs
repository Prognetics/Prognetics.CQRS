using System.Threading.Tasks;
using Autofac;
using Prognetics.CQRS.Simplified;
using Xunit;

namespace Prognetics.CQRS.Tests.Simplified.Integration.Event
{
    public class EventTests : SimplifiedTestsBase
    {
        [Fact]
        public async Task FireEvent()
        {
            using var scope = Container.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();

            await mediator.Publish(new NumberEvent(2));
        }
    }
}