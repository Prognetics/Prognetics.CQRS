using System;
using System.Threading.Tasks;
using Prognetics.CQRS.Handlers;

namespace Prognetics.CQRS.Tests.Shared.GenericCommand
{
    public class TestGenericCommandHandler<TR> : GenericCommandHandlerAsync<TestGenericCommand<TR>, TR> where TR : class
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task HandleAsync(TestGenericCommand<TR> command)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            Console.WriteLine(command.Data);
        }
    }
}