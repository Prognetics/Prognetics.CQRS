namespace Prognetics.CQRS.Tests.Shared.Commands.Simple;

public class SumCommand : ICommand
{
    public SumCommand(int first, int second)
    {
        First = first;
        Second = second;
    }

    public int First { get; }
    public int Second { get; }
}