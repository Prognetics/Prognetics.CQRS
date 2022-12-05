using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Tests.Simplified.Shared.GenericQuery
{
    public class SampleGenericQuery<T> : IQuery<int>
    {
        public SampleGenericQuery(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}