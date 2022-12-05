using Prognetics.CQRS.Simplified;
using System;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Simplified.Shared.GenericCommand
{
    public class TestGenericCommandHandler<T> : IAsyncCommandHandler<TestGenericCommand<T>>
    {
        public Task Handle(TestGenericCommand<T> command)
        {
            Console.WriteLine(command.Data);
            return Task.CompletedTask;
        }
    }
}