using Autofac;
using Autofac.Core.Registration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Prognetics.CQRS.Tests.Shared;
using Prognetics.CQRS.Tests.Shared.Commands.Generic;
using Prognetics.CQRS.Tests.Shared.Commands.Simple;
using Prognetics.CQRS.Tests.Shared.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Prognetics.CQRS.Tests.Autofac;

public class MicrosoftDICommandTests : IClassFixture<MicrosoftDIFixture>
{
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;
    private readonly IServiceForTests _serviceMock;

    public MicrosoftDICommandTests(MicrosoftDIFixture fixture)
    {
        _scope = fixture.ServiceProvider.CreateScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        _serviceMock = _scope.ServiceProvider.GetRequiredService<IServiceForTests>();
    }

    [Fact]
    public void AnyCommandShouldNotBeFoundAndExceptionShouldBeThrown()
    {
        // Act
        var sendingAny = () => _mediator.Send(new NotRegisteredCommand());
        var sendingAnyAsync = () => _mediator.SendAsync(new NotRegisteredCommand());
        var sendingRegular = () => _mediator.Send(new NotRegisteredRegularCommand());
        var sendingAsync = () => _mediator.SendAsync(new NotRegisteredAsyncCommand());
        var sendingGeneric = () => _mediator.Send(new GenericCommand<SimpleData>(new("Hello")));
        var sendingGenericAsync = () => _mediator.SendAsync(new GenericCommand<SimpleData>(new("Hello")));

        // Assert
        Assert.ThrowsAny<InvalidOperationException>(sendingAny);
        Assert.ThrowsAnyAsync<InvalidOperationException>(sendingAnyAsync);
        Assert.ThrowsAnyAsync<InvalidOperationException>(sendingAsync);
        Assert.ThrowsAny<InvalidOperationException>(sendingRegular);
        Assert.ThrowsAny<InvalidOperationException>(sendingGeneric);
        Assert.ThrowsAny<InvalidOperationException>(sendingGenericAsync);
    }

    [Fact]
    public async Task AsyncCommandShouldBeFoundAndExecutesCorrectly()
    {
        // Act
        await _mediator.SendAsync(new SumCommand(2, 3));

        // Assert
        _serviceMock.Received(1).Execute(5);
    }

    [Fact]
    public void CommandShouldBeFoundAndExecutesServiceWithProperValue()
    {
        // Act
        _mediator.Send(new SumCommand(2, 3));

        // Assert
        _serviceMock.Received(1).Execute(5);
    }

    [Fact]
    public void StringGenericCommandShouldBeFoundAndExecutedCorrectly()
    {
        // Arrange
        var data = "Hello!";
        var command = new GenericCommand<string>(data);

        // Act
        _mediator.Send(command);

        // Assert
        _serviceMock.Received(1).Execute(data.ToUpper());
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}