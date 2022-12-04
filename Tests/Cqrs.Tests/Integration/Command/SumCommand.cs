using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Tests.Integration.Command
{
    public class SumCommand : ICommandAsync
    {
        public SumCommand(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}