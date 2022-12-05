using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Tests.Simplified.Integration.Query
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