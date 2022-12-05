using Autofac;
using FluentAssertions;
using Prognetics.CQRS.Simplified.Tests.Shared.GenericQuery;
using Prognetics.CQRS.Simplified.Tests.Shared.Modules;
using Xunit;

namespace Prognetics.CQRS.Simplified.Tests.Integration.GenericQuery
{
    public class GenericQueryTests
    {
        private readonly IContainer _container;

        public GenericQueryTests()
        {
            _container = BuildContainer();
        }

        [Fact]
        public void ShouldExecuteQueryAndReturnData()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                var query = new SampleGenericQuery<int>(2);

                var result = mediator.Fetch<SampleGenericQuery<int>, int>(query).Result;

                result.Should().Be(5);
            }
        }

        [Fact]
        public void ShouldExecuteTwoQueriesWithDifferentTypesPassed()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                var queryOne = new SampleGenericQuery<int>(2);
                var resultOne = mediator.Fetch<SampleGenericQuery<int>, int>(queryOne).Result;

                var queryTwo = new SampleGenericQuery<string>("7");
                var resultTwo = mediator.Fetch<SampleGenericQuery<string>, int>(queryTwo).Result;

                resultOne.Should().Be(5);
                resultTwo.Should().Be(10);
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