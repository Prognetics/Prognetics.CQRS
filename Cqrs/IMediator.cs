using System.Threading.Tasks;

namespace Prognetics.CQRS;

public interface IMediator
{
    TResult Fetch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    Task<TResult> FetchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    void Send<TCommand>(TCommand command) where TCommand : ICommand;
    Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
}