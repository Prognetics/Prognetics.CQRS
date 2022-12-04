using System.Threading.Tasks;
using Prognetics.CQRS.Handlers;

namespace Prognetics.CQRS.Tests.Integration.Event
{
    public class SumNumberEventHandler : EventHandlerAsync<NumberEvent>
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task HandleAsync(NumberEvent @event)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var result = @event.Number + 3;
        }
    }
}