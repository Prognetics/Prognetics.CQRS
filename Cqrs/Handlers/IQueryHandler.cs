using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Handlers
{
    public interface IQueryHandler<in TQuery, out TResult> : IQueryHandler where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}