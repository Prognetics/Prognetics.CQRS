using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Tests.Shared.GenericCommand
{
    public class TestGenericCommand<T> : IGenericCommandAsync<T> where T : class
    {
        public TestGenericCommand(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}