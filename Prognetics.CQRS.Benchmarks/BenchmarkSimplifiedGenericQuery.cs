using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Benchmarks;

internal class BenchmarkSimplifiedGenericQuery<T> : IQuery<int>
{
    public BenchmarkSimplifiedGenericQuery(T data)
    {
        Data = data;
    }

    public T Data { get; }
}

internal class BenchmarkSimplifiedGenericQueryHandler<T> : IQueryHandler<BenchmarkSimplifiedGenericQuery<T>, int>
{
    public int Handle(BenchmarkSimplifiedGenericQuery<T> query)
    {
        var data = 0;
        for (int i = 0; i < 1e6; i++)
        {
            data = Convert.ToInt32(query.Data?.ToString());
        }

        return data + 3;
    }
}
