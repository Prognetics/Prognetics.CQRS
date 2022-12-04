using System.Threading.Tasks;
using Prognetics.CQRS.Handlers;

namespace Prognetics.CQRS.Tests.Integration.QueryAsync
{
    public class SumQueryHandlerAsync : IQueryHandlerAsync<SumQueryAsync, int>
    {
        public Task<int> Handle(SumQueryAsync query)
        {
            return Task.FromResult(2 + query.Number);
        }
    }
}