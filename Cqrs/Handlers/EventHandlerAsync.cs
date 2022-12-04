using Prognetics.CQRS.Markers;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Handlers
{
    public class EventHandlerAsync<TEvent> : IEventHandlerAsync<TEvent> where TEvent : IEventAsync
    {
#pragma warning disable 1998
        public virtual async Task HandleAsync(TEvent @event)
#pragma warning restore 1998
        { }
    }
}