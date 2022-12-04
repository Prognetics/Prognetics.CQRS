namespace Prognetics.CQRS.Tests.Integration.Command
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