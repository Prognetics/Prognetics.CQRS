using System.Threading.Tasks;

namespace Prognetics.CQRS;

public interface IEventHandler<TEvent> where TEvent : IEvent
{
    Task Handle(TEvent @event);
}
