using System.Threading.Tasks;

namespace Prognetics.CQRS;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query);
}
