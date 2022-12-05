using System.Threading.Tasks;

namespace Prognetics.CQRS;

public interface IMediator
{
    Task<TResult> Fetch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
}
