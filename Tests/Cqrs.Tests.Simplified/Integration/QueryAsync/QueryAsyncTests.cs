using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Prognetics.CQRS.Simplified.Tests.Shared.Modules;
using Xunit;

namespace Prognetics.CQRS.Simplified.Tests.Integration.QueryAsync
{
    public class QueryAsyncTests
    {
        private readonly IContainer _container;

        public QueryAsyncTests()
        {
            _container = BuildContainer();
        }

        [Fact]
        public async Task ShouldReturnSumResultWithRegularFetch()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var result = await mediator.Fetch<SumQueryAsync, int>(new SumQueryAsync(3));

                result.Should().Be(5);
            }
        }

        [Fact]
        public async Task ShouldReturnSumResultWithFetchFast()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var query = new SumQueryAsync(3);
                var result = await mediator.Fetch<SumQueryAsync, int>(query);

                result.Should().Be(5);
            }
        }

        private IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new CqrsModule("Prognetics.CQRS.Simplified.Tests"));
            return containerBuilder.Build();
        }
    }
}