using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prognetics.CQRS;
public interface IMediator
{
    Task<TResult> Fetch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
}


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

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query);
}

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand query);
}

public interface IEventHandler<TEvent> where TEvent : IEvent
{
    Task Handle(TEvent @event);
}

public interface IHandlerResolver
{
    T Resolve<T>();
}

public interface IEvent
{ }

public interface IQuery<T>
{ }

public interface ICommand
{ }
