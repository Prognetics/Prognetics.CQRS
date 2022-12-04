using System;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Shared.GenericCommand
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