using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Tests.Simplified.Integration.Command
{
    public class SumCommand : ICommand
    {
        public SumCommand(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}