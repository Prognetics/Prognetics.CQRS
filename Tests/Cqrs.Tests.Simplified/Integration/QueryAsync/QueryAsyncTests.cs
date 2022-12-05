using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Prognetics.CQRS.Simplified;
using Xunit;

namespace Prognetics.CQRS.Tests.Simplified.Integration.QueryAsync
{
    public class QueryAsyncTests : SimplifiedTestsBase
    {
        [Fact]
        public async Task ShouldReturnSumResultWithRegularFetch()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var result = await mediator.FetchAsync<SumQueryAsync, int>(new SumQueryAsync(3));

                result.Should().Be(5);
            }
        }

        [Fact]
        public async Task ShouldReturnSumResultWithFetchFast()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var query = new SumQueryAsync(3);
                var result = await mediator.FetchAsync<SumQueryAsync, int>(query);

                result.Should().Be(5);
            }
        }
    }
}