using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Tests.Integration.QueryAsync
{
    public class SumQueryAsync : IQueryAsync<int>
    {
        public SumQueryAsync(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}