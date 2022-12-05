using Autofac;
using FluentAssertions;
using Prognetics.CQRS.Mediator;
using Prognetics.CQRS.Tests.Shared.Modules;
using Xunit;

namespace Prognetics.CQRS.Tests.Integration.Query
{
    public class QueryTests
    {
        private readonly IContainer _container;

        public QueryTests()
        {
            _container = BuildContainer();
        }

        [Fact]
        public void ExecuteQueryWithReflection()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var result = mediator.Fetch(new SumQuery(2));

                result.Should().Be(5);
            }
        }

        [Fact]
        public void ExecuteQueryWithoutReflection()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var query = new SumQuery(2);
                var result = mediator.FetchFast<SumQuery, int>(query);

                result.Should().Be(5);
            }
        }

        private IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new CqrsModule("Prognetics.Cqrs.Tests"));
            return containerBuilder.Build();
        }
    }
}