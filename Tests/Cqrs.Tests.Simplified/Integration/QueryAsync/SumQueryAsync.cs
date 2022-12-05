using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Tests.Simplified.Integration.QueryAsync
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