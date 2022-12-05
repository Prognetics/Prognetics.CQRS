using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Tests.Simplified.Integration.Query
{
    public class SumQueryHandler : IQueryHandler<SumQuery, int>
    {
        public int Handle(SumQuery query)
        {
            return query.Number + 3;
        }
    }
}