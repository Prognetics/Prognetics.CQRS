using FluentAssertions;
using Prognetics.CQRS.Tests.Shared.Querys.Simple;
using Prognetics.CQRS.Tests.Shared.Queries.Generic;
using Prognetics.CQRS.Tests.Shared.Queries.Simple;
using System.Threading.Tasks;
using Xunit;
using Prognetics.CQRS.Tests.Shared.Commands.Simple;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Prognetics.CQRS.Tests.Autofac;

public class MicrosoftDIQueriesTests : IClassFixture<MicrosoftDIFixture>
{
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;

    public MicrosoftDIQueriesTests(MicrosoftDIFixture fixture)
    {
        _scope = fixture.ServiceProvider.CreateScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public void AnyQueryShouldNotBeFoundAndExceptionShouldBeThrown()
    {
        // Act
        var fetchingAny = () => _mediator.Fetch<NotRegisteredQuery, string>(new NotRegisteredQuery());
        var fetchingAnyAsync = () => _mediator.FetchAsync<NotRegisteredQuery, string>(new NotRegisteredQuery());
        var fetchingRegular = () => _mediator.Fetch<NotRegisteredRegularQuery, string>(new NotRegisteredRegularQuery());
        var fetchingAsync = () => _mediator.FetchAsync<NotRegisteredAsyncQuery, string>(new NotRegisteredAsyncQuery());
        var fetchingGeneric = () => { _mediator.Fetch<SampleGenericQuery<int>, int>(new SampleGenericQuery<int>(7)); };
        var fetchingGenericAsync = () => _mediator.FetchAsync<SampleGenericQuery<string>, int>(new SampleGenericQuery<string>("Hello"));

        // Assert
        Assert.ThrowsAny<InvalidOperationException>(fetchingAny);
        Assert.ThrowsAnyAsync<InvalidOperationException>(fetchingAnyAsync);
        Assert.ThrowsAny<InvalidOperationException>(fetchingRegular);
        Assert.ThrowsAnyAsync<InvalidOperationException>(fetchingAsync);
        Assert.ThrowsAny<InvalidOperationException>(fetchingGeneric);
        Assert.ThrowsAnyAsync<InvalidOperationException>(fetchingGenericAsync);
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

    public void Dispose()
    {
        _scope.Dispose();
    }
}