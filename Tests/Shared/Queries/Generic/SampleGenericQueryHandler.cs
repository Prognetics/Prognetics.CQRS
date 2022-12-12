using System;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Shared.Queries.Generic;

public class SampleGenericQueryHandler<T> :
    IQueryHandler<SampleGenericQuery<T>, int>,
    IAsyncQueryHandler<SampleGenericQuery<T>, int>
{
    public int Handle(SampleGenericQuery<T> query)
    {
        var data = 0;
        for (int i = 0; i < 1e6; i++)
        {
            data = Convert.ToInt32(query.Data.ToString());
        }

        return data;
    }

    Task<int> IAsyncQueryHandler<SampleGenericQuery<T>, int>.Handle(SampleGenericQuery<T> query)
    {
        var data = 0;
        for (int i = 0; i < 1e6; i++)
        {
            data = Convert.ToInt32(query.Data.ToString());
        }

        return Task.FromResult(data);
    }
}