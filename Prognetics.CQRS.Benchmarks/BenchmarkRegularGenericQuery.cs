using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Benchmarks;

internal class BenchmarkRegularGenericQuery<T> : IGenericQuery<int, T>
{
    public BenchmarkRegularGenericQuery(T data)
    {
        Data = data;
    }

    public T Data { get; }
}

internal class BenchmarkRegularGenericQueryHandler<T> : IGenericQueryHandler<BenchmarkRegularGenericQuery<T>, int, T>
{
    public int Handle(BenchmarkRegularGenericQuery<T> query)
    {
        var data = 0;
        for (int i = 0; i < 1e6; i++)
        {
            data = Convert.ToInt32(query.Data?.ToString());
        }

        return data + 3;
    }
}
