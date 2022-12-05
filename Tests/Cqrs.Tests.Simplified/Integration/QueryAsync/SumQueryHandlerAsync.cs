using System.Threading.Tasks;

namespace Prognetics.CQRS.Simplified.Tests.Integration.QueryAsync
{
    public class SumQueryHandlerAsync : IQueryHandler<SumQueryAsync, int>
    {
        public Task<int> Handle(SumQueryAsync query)
        {
            return Task.FromResult(2 + query.Number);
        }
    }
}