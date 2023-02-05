using Autofac;
using FluentAssertions;
using Prognetics.CQRS.Tests.Shared.Querys.Simple;
using Prognetics.CQRS.Tests.Shared.Queries.Generic;
using Prognetics.CQRS.Tests.Shared.Queries.Simple;
using System.Threading.Tasks;
using Xunit;
using Prognetics.CQRS.Tests.Shared.Commands.Simple;
using Autofac.Core.Registration;

namespace Prognetics.CQRS.Tests.Autofac;

public class AutofacQueriesTests : IClassFixture<AutofacFixture>
{
    private readonly ILifetimeScope _scope;
    private readonly IMediator _mediator;

    public AutofacQueriesTests(AutofacFixture fixture)
    {
        _scope = fixture.Container.BeginLifetimeScope();
        _mediator = _scope.Resolve<IMediator>();
    }

    [Fact]
    public void AnyQueryShouldNotBeFoundAndExceptionShouldBeThrown()
    {
        // Act
        var fetchingAny = () => _mediator.Fetch<NotRegisteredQuery, string>(new NotRegisteredQuery());
        var fetchingAnyAsync = () => _mediator.FetchAsync<NotRegisteredQuery, string>(new NotRegisteredQuery());
        var fetchingAsync = () => _mediator.FetchAsync<NotRegisteredAsyncQuery, string>(new NotRegisteredAsyncQuery());
        var fetchingRegular = () => _mediator.Fetch<NotRegisteredRegularQuery, string>(new NotRegisteredRegularQuery());

        // Assert
        Assert.ThrowsAny<ComponentNotRegisteredException>(fetchingAny);
        Assert.ThrowsAnyAsync<ComponentNotRegisteredException>(fetchingAnyAsync);
        Assert.ThrowsAnyAsync<ComponentNotRegisteredException>(fetchingAsync);
        Assert.ThrowsAny<ComponentNotRegisteredException>(fetchingRegular);
    }

    [Fact]
    public void ExecuteQueryShouldReturnCorrectValue()
    {
        // Arrange
        var query = new SquareQuery(2);

        // Act
        var result = _mediator.Fetch<SquareQuery, int>(query);

        // Assert
        result.Should().Be(4);
    }

    [Fact]
    public async Task ExecuteQueryAsyncShouldReturnCorrectValue()
    {
        // Arrange
        var query = new SquareQuery(5);

        // Act
        var result = await _mediator.FetchAsync<SquareQuery, int>(query);

        // Assert
        result.Should().Be(25);
    }

    [Fact]
    public void ExecuteGenericQueryShouldReturnCorrectValue()
    {
        // Arrange
        var query = new SampleGenericQuery<int>(7);

        // Act
        var result = _mediator.Fetch<SampleGenericQuery<int>, int>(query);

        // Assert
        result.Should().Be(7);
    }

    [Fact]
    public async Task ExecuteGenericAsyncQueryShouldReturnCorrectValue()
    {
        // Arrange
        var query = new SampleGenericQuery<int>(0);

        // Act
        var result = await _mediator.FetchAsync<SampleGenericQuery<int>, int>(query);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void ExecuteTwoGenericQueriesWithDifferentTypesShouldPassedAndReturnCorrectValues()
    {
        // Act
        var queryOne = new SampleGenericQuery<int>(2);
        var resultOne = _mediator.Fetch<SampleGenericQuery<int>, int>(queryOne);

        // Arrange
        var queryTwo = new SampleGenericQuery<string>("7");
        var resultTwo = _mediator.Fetch<SampleGenericQuery<string>, int>(queryTwo);

        // Assert
        resultOne.Should().Be(2);
        resultTwo.Should().Be(7);
    }

    [Fact]
    public async Task ExecuteTwoGenericAsyncQueriesWithDifferentTypesShouldPassedAndReturnCorrectValues()
    {
        // Act
        var queryOne = new SampleGenericQuery<int>(2);
        var resultOne = await _mediator.FetchAsync<SampleGenericQuery<int>, int>(queryOne);

        // Arrange
        var queryTwo = new SampleGenericQuery<string>("7");
        var resultTwo = await _mediator.FetchAsync<SampleGenericQuery<string>, int>(queryTwo);

        // Assert
        resultOne.Should().Be(2);
        resultTwo.Should().Be(7);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}