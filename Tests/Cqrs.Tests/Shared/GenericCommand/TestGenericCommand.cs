namespace Prognetics.CQRS.Tests.Shared.GenericCommand
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