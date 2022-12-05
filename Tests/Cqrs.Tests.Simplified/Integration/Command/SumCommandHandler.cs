using Prognetics.CQRS.Simplified;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Simplified.Integration.Command
{
    public class SumCommandHandler : IAsyncCommandHandler<SumCommand>
    {
        public Task Handle(SumCommand command)
        {
            var result = 2 + command.Number;
            return Task.CompletedTask;
        }
    }
}