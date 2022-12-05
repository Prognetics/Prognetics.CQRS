using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Tests.Shared.GenericQuery
{
    public class SampleGenericQuery<T> : IGenericQuery<int, T>
    {
        public SampleGenericQuery(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}