using System.Threading.Tasks;
using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Mediator
{
    public interface IMediator
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommandAsync;

        Task SendGenericAsync<TCommand, TObject>(TCommand command) where TCommand : IGenericCommandAsync<TObject>;

        TResult Fetch<TResult>(IQuery<TResult> query);

        Task<TResult> FetchAsync<TResult>(IQueryAsync<TResult> query);

        TResult FetchFast<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;

        Task<TResult> FetchFastAsync<TQuery, TResult>(TQuery query) where TQuery : IQueryAsync<TResult>;

        TResult FetchGeneric<TQuery, TResult, T>(TQuery query) where TQuery : IGenericQuery<TResult, T>;

        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEventAsync;
    }
}