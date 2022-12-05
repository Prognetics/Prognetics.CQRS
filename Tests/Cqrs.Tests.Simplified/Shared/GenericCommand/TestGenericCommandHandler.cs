using System;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Simplified.Tests.Shared.GenericCommand
{
    public class TestGenericCommandHandler<T> : ICommandHandler<TestGenericCommand<T>>
    {
        public Task Handle(TestGenericCommand<T> command)
        {
            Console.WriteLine(command.Data);
            return Task.CompletedTask;
        }
    }
}