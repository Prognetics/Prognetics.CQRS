namespace Prognetics.CQRS.Tests.Integration.QueryAsync
{
    public class SumQueryAsync : IQuery<int>
    {
        public SumQueryAsync(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}