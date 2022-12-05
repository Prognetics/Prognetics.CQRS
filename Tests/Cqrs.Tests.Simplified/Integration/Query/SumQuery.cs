namespace Prognetics.CQRS.Simplified.Tests.Integration.Query
{
    public class SumQuery : IQuery<int>
    {
        public SumQuery(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}