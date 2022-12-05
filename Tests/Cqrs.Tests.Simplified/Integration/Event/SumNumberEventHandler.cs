using System.Threading.Tasks;

namespace Prognetics.CQRS.Simplified.Tests.Integration.Event
{
    public class SumNumberEventHandler : IEventHandler<NumberEvent>
    {
        public Task Handle(NumberEvent @event)
        {
            var result = @event.Number + 3;
            return Task.CompletedTask;
        }
    }
}