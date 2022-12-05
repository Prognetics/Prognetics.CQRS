using System.Threading.Tasks;

namespace Prognetics.CQRS.Simplified.Tests.Integration.Query
{
    public class SumQueryHandler : IQueryHandler<SumQuery, int>
    {
        public Task<int> Handle(SumQuery query)
        {
            return Task.FromResult(query.Number + 3);
        }
    }
}