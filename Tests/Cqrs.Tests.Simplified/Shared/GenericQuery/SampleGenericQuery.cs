namespace Prognetics.CQRS.Simplified.Tests.Shared.GenericQuery
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