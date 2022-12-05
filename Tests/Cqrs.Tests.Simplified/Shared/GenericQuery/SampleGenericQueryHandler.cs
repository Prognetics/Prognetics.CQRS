using Prognetics.CQRS.Simplified;
using System;

namespace Prognetics.CQRS.Tests.Simplified.Shared.GenericQuery
{
    public class SampleGenericQueryHandler<T> : IQueryHandler<SampleGenericQuery<T>, int>
    {
        public int Handle(SampleGenericQuery<T> query)
        {
            var data = 0;
            for (int i = 0; i < 1e6; i++)
            {
                data = Convert.ToInt32(query.Data.ToString());
            }

            return data + 3;
        }
    }
}