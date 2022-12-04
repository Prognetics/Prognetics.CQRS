namespace Prognetics.CQRS.Markers
{
    public interface IGenericQueryHandler<in TQuery, out TResult, in T> where TQuery : IGenericQuery<TResult, T>
    {
        TResult Handle(TQuery query);
    }
}