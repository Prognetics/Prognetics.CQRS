﻿using Autofac;
using FluentAssertions;
using Prognetics.CQRS.Mediator;
using Prognetics.CQRS.Tests.Shared.GenericQuery;
using Prognetics.CQRS.Tests.Shared.Modules;
using Xunit;

namespace Prognetics.CQRS.Tests.Integration.GenericQuery
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

                var result = mediator.FetchGeneric<SampleGenericQuery<int>, int, int>(query);

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
                var resultOne = mediator.FetchGeneric<SampleGenericQuery<int>, int, int>(queryOne);

                var queryTwo = new SampleGenericQuery<string>("7");
                var resultTwo = mediator.FetchGeneric<SampleGenericQuery<string>, int, string>(queryTwo);

                resultOne.Should().Be(5);
                resultTwo.Should().Be(10);
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