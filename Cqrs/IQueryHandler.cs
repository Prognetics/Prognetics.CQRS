using System.Threading.Tasks;

namespace Prognetics.CQRS;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    TResult Handle(TQuery query);
}

public interface IAsyncQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query);
}