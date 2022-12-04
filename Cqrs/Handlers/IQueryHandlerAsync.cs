using Prognetics.CQRS.Markers;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Handlers
{
    public interface IQueryHandlerAsync<in TQuery, TResult> : IQueryHandlerAsync where TQuery : IQueryAsync<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}