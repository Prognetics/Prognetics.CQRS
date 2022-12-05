using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Tests.Simplified.Shared.GenericCommand
{
    public class TestGenericCommand<T> : ICommand
    {
        public TestGenericCommand(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}