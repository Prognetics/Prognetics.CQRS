namespace Prognetics.CQRS.Tests.Shared.Queries.Simple;

public class SquareQuery : IQuery<int>
{
    public SquareQuery(int number)
    {
        Number = number;
    }

    public int Number { get; }
}