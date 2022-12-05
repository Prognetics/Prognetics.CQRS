using System.Threading.Tasks;

namespace Prognetics.CQRS.Simplified.Tests.Integration.Command
{
    public class SumCommandHandler : ICommandHandler<SumCommand>
    {
        public Task Handle(SumCommand command)
        {
            var result = 2 + command.Number;
            return Task.CompletedTask;
        }
    }
}