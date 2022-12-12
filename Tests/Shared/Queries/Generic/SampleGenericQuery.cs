namespace Prognetics.CQRS.Tests.Shared.Queries.Generic;

public class SampleGenericQuery<T> : IQuery<int>
{
    public SampleGenericQuery(T data)
    {
        Data = data;
    }

    public T Data { get; }
}