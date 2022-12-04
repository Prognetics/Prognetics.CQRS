using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Handlers
{
    public abstract class GenericQueryHandler<TQuery, TResult, T> : IGenericQueryHandler<TQuery, TResult, T> where TQuery : IGenericQuery<TResult, T>
    {
        public virtual TResult Handle(TQuery query)
        {
            return default;
        }
    }
}