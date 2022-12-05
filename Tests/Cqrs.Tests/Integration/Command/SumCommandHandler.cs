using System.Threading.Tasks;
using Prognetics.CQRS.Handlers;

namespace Prognetics.CQRS.Tests.Integration.Command
{
    public class SumCommandHandler : CommandHandlerAsync<SumCommand>
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task HandleAsync(SumCommand command)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var result = 2 + command.Number;
        }
    }
}