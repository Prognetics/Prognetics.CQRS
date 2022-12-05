using System.Threading.Tasks;

namespace Prognetics.CQRS.Simplified.Tests.Integration.Event
{
    public class MultiplyNumberEventHandler : IEventHandler<NumberEvent>
    {
#pragma warning disable 1998
        public Task Handle(NumberEvent @event)
#pragma warning restore 1998
        {
            var result = @event.Number * 2;
            return Task.CompletedTask;
        }
    }
}