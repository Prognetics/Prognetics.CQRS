using Autofac;
using FluentAssertions;
using Prognetics.CQRS.Simplified;
using Prognetics.CQRS.Tests.Simplified.Shared.GenericQuery;
using Xunit;

namespace Prognetics.CQRS.Tests.Simplified.Integration.GenericQuery
{
    public class GenericQueryTests : SimplifiedTestsBase
    {
        [Fact]
        public void ShouldExecuteQueryAndReturnData()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                var query = new SampleGenericQuery<int>(2);

                var result = mediator.Fetch<SampleGenericQuery<int>, int>(query);

                result.Should().Be(5);
            }
        }

        [Fact]
        public void ShouldExecuteTwoQueriesWithDifferentTypesPassed()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var queryOne = new SampleGenericQuery<int>(2);
                var resultOne = mediator.Fetch<SampleGenericQuery<int>, int>(queryOne);

                var queryTwo = new SampleGenericQuery<string>("7");
                var resultTwo = mediator.Fetch<SampleGenericQuery<string>, int>(queryTwo);

                resultOne.Should().Be(5);
                resultTwo.Should().Be(10);
            }
        }
    }
}