using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prognetics.CQRS.Handlers;
using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Mediator
{
    public class Mediator : IMediator
    {
        private readonly Func<Type, ICommandHandlerAsync> _commandHandlersAsyncFactory;
        private readonly Func<Type, object> _genericHandlerFactory;
        private readonly Func<Type, IQueryHandler> _queryHandlersFactory;
        private readonly Func<Type, IQueryHandlerAsync> _queryHandlerAsyncFactory;
        private readonly Func<Type, IEnumerable<IEventHandlerAsync>> _eventHandlersFactory;

        public Mediator(Func<Type, IQueryHandler> queryHandlersFactory,
            Func<Type, IEnumerable<IEventHandlerAsync>> eventHandlersFactory,
            Func<Type, ICommandHandlerAsync> commandHandlersAsyncFactory,
            Func<Type, object> genericHandlerFactory,
            Func<Type, IQueryHandlerAsync> queryHandlerAsyncFactory)
        {
            _queryHandlersFactory = queryHandlersFactory;
            _eventHandlersFactory = eventHandlersFactory;
            _commandHandlersAsyncFactory = commandHandlersAsyncFactory;
            _genericHandlerFactory = genericHandlerFactory;
            _queryHandlerAsyncFactory = queryHandlerAsyncFactory;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommandAsync
        {
            var handler = (ICommandHandlerAsync<TCommand>)_commandHandlersAsyncFactory(typeof(TCommand));
            await handler.HandleAsync(command);
        }

        public async Task SendGenericAsync<TCommand, TObject>(TCommand command) where TCommand : IGenericCommandAsync<TObject>
        {
            var handler = (IGenericCommandHandlerAsync<TCommand>)_genericHandlerFactory(typeof(TCommand));
            await handler.HandleAsync(command);
        }

        public TResult Fetch<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            var resultType = typeof(TResult);

            var fetchMethod = GetType().GetMethod("FetchFast").MakeGenericMethod(new Type[] { queryType, resultType });

            return (TResult)fetchMethod.Invoke(this, new object[] { query });
        }

        public async Task<TResult> FetchAsync<TResult>(IQueryAsync<TResult> query)
        {
            var queryType = query.GetType();
            var resultType = typeof(TResult);

            var fetchMethod = GetType().GetMethod("FetchFastAsync").MakeGenericMethod(new Type[] { queryType, resultType });

            return await (Task<TResult>)fetchMethod.Invoke(this, new object[] { query });
        }

        public TResult FetchFast<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = (IQueryHandler<TQuery, TResult>)_queryHandlersFactory(query.GetType());
            return handler.Handle(query);
        }

        public async Task<TResult> FetchFastAsync<TQuery, TResult>(TQuery query) where TQuery : IQueryAsync<TResult>
        {
            var handler = (IQueryHandlerAsync<TQuery, TResult>)_queryHandlerAsyncFactory(query.GetType());
            return await handler.Handle(query);
        }

        public TResult FetchGeneric<TQuery, TResult, T>(TQuery query) where TQuery : IGenericQuery<TResult, T>
        {
            var handler = (IGenericQueryHandler<TQuery, TResult, T>)_genericHandlerFactory(typeof(TQuery));
            return handler.Handle(query);
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEventAsync
        {
            var handlers = _eventHandlersFactory(typeof(TEvent))
                .Cast<IEventHandlerAsync<TEvent>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}