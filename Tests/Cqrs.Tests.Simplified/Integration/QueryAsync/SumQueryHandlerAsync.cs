using Prognetics.CQRS.Simplified;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Simplified.Integration.QueryAsync
{
    public class SumQueryHandlerAsync : IAsyncQueryHandler<SumQueryAsync, int>
    {
        public Task<int> Handle(SumQueryAsync query)
        {
            return Task.FromResult(2 + query.Number);
        }
    }
}