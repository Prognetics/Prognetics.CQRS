using Prognetics.CQRS.Handlers;

namespace Prognetics.CQRS.Tests.Integration.Query
{
    public class SumQueryHandler : IQueryHandler<SumQuery, int>
    {
        public int Handle(SumQuery query)
        {
            return query.Number + 3;
        }
    }
}