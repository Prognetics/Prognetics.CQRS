using System;
using Prognetics.CQRS.Handlers;

namespace Prognetics.CQRS.Tests.Shared.GenericQuery
{
    public class SampleGenericQueryHandler<T> : GenericQueryHandler<SampleGenericQuery<T>, int, T>
    {
        public override int Handle(SampleGenericQuery<T> query)
        {
            var data = 0;
            for(int i = 0; i < 1e6; i++)
            {
                data = Convert.ToInt32(query.Data.ToString());
            }

            return data + 3;
        }
    }
}