using System.Threading.Tasks;
using Autofac;
using Prognetics.CQRS.Simplified;
using Xunit;

namespace Prognetics.CQRS.Tests.Simplified.Integration.Command
{
    public class CommandTests : SimplifiedTestsBase
    {
        [Fact]
        public async Task CommandShouldBeFoundAndExecuted()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                await mediator.SendAsync(new SumCommand(2));
            }
        }
    }
}