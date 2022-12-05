using System.Threading.Tasks;
using Prognetics.CQRS.Handlers;

namespace Prognetics.CQRS.Tests.Integration.Event
{
    public class MultiplyNumberEventHandler : EventHandlerAsync<NumberEvent>
    {
#pragma warning disable 1998
        public override async Task HandleAsync(NumberEvent @event)
#pragma warning restore 1998
        {
            var result = @event.Number * 2;
        }
    }
}