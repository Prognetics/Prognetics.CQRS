using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Simplified;

public class Mediator : IMediator
{
    private readonly IHandlerResolver _resolver;

    public Mediator(IHandlerResolver resolver)
    {
        _resolver = resolver;
    }

    public TResult Fetch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        var handler = _resolver.Resolve<IQueryHandler<TQuery, TResult>>();
        return handler.Handle(query);
    }

    public Task<TResult> FetchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        var handler = _resolver.Resolve<IAsyncQueryHandler<TQuery, TResult>>();
        return handler.Handle(query);
    }

    public Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _resolver.Resolve<IAsyncCommandHandler<TCommand>>();
        return handler.Handle(command);
    }

    public void Send<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _resolver.Resolve<ICommandHandler<TCommand>>();
        handler.Handle(command);
    }

    public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var handlers = _resolver.Resolve<IEnumerable<IEventHandler<TEvent>>>();
        foreach (var handler in handlers)
        {
            await handler.Handle(@event);
        }
    }
}
