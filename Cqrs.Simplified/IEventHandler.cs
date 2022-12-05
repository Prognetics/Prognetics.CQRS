using System.Threading.Tasks;

namespace Prognetics.CQRS.Simplified;

public interface IEventHandler<TEvent> where TEvent : IEvent
{
    Task Handle(TEvent @event);
}
