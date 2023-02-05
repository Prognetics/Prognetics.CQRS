using System;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Shared.Queries.Simple;

public class SquareQueryHandler :
    IQueryHandler<SquareQuery, int>,
    IAsyncQueryHandler<SquareQuery, int>
{
    public int Handle(SquareQuery query)
    {
        return (int)Math.Pow(query.Number, 2);
    }

    Task<int> IAsyncQueryHandler<SquareQuery, int>.Handle(SquareQuery query)
    {
        return Task.FromResult(Handle(query));
    }
}