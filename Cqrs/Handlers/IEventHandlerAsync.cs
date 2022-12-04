using System.Threading.Tasks;
using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Handlers
{
    public interface IEventHandlerAsync<in TEvent> : IEventHandlerAsync where TEvent : IEventAsync
    {
        Task HandleAsync(TEvent @event);
    }
}