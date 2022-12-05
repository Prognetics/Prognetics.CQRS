using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prognetics.CQRS;

public class Mediator : IMediator
{
    private readonly IHandlerResolver _resolver;

    public Mediator(IHandlerResolver resolver)
    {
        _resolver = resolver;
    }

    public Task<TResult> Fetch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        var handler = _resolver.Resolve<IQueryHandler<TQuery, TResult>>();
        return handler.Handle(query);
    }

    public Task Send<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _resolver.Resolve<ICommandHandler<TCommand>>();
        return handler.Handle(command);
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
