using System;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Shared.GenericQuery
{
    public class SampleGenericQueryHandler<T> : IQueryHandler<SampleGenericQuery<T>, int>
    {
        public Task<int> Handle(SampleGenericQuery<T> query)
        {
            var data = Convert.ToInt32(query.Data.ToString());

            return Task.FromResult(data + 3);
        }
    }
}