using Autofac;
using FluentAssertions;
using Prognetics.CQRS.Simplified;
using Xunit;

namespace Prognetics.CQRS.Tests.Simplified.Integration.Query
{
    public class QueryTests : SimplifiedTestsBase
    {
        [Fact]
        public void ExecuteQueryWithReflection()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var result = mediator.Fetch<SumQuery, int>(new SumQuery(2));

                result.Should().Be(5);
            }
        }

        [Fact]
        public void ExecuteQueryWithoutReflection()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var query = new SumQuery(2);
                var result = mediator.Fetch<SumQuery, int>(query);

                result.Should().Be(5);
            }
        }
    }
}