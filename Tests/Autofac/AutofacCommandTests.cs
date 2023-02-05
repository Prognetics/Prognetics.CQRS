using Autofac;
using Autofac.Core.Registration;
using NSubstitute;
using Prognetics.CQRS.Tests.Shared;
using Prognetics.CQRS.Tests.Shared.Commands.Generic;
using Prognetics.CQRS.Tests.Shared.Commands.Simple;
using Prognetics.CQRS.Tests.Shared.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Prognetics.CQRS.Tests.Autofac;

public class AutofacCommandTests : IClassFixture<AutofacFixture>
{
    private readonly ILifetimeScope _scope;
    private readonly IMediator _mediator;
    private readonly IServiceForTests _serviceMock;

    public AutofacCommandTests(AutofacFixture fixture)
    {
        _scope = fixture.Container.BeginLifetimeScope();
        _mediator = _scope.Resolve<IMediator>();
        _serviceMock = _scope.Resolve<IServiceForTests>();
    }

    [Fact]
    public void AnyCommandShouldNotBeFoundAndExceptionShouldBeThrown()
    {
        // Act
        var sendingAny = () => _mediator.Send(new NotRegisteredCommand());
        var sendingAnyAsync = () => _mediator.SendAsync(new NotRegisteredCommand());
        var sendingAsync = () => _mediator.SendAsync(new NotRegisteredAsyncCommand());
        var sendingRegular = () => _mediator.Send(new NotRegisteredRegularCommand());

        // Assert
        Assert.ThrowsAny<ComponentNotRegisteredException>(sendingAny);
        Assert.ThrowsAnyAsync<ComponentNotRegisteredException>(sendingAnyAsync);
        Assert.ThrowsAnyAsync<ComponentNotRegisteredException>(sendingAsync);
        Assert.ThrowsAny<ComponentNotRegisteredException>(sendingRegular);
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
    public async Task AsyncGenericCommandShouldBeFoundAndExecutedCorrectly()
    {
        // Arrange
        var data = new SimpleData("Hello!");

        var command = new GenericCommand<SimpleData>(data);

        // Act
        await _mediator.SendAsync(command);

        // Assert
        _serviceMock.Received(1).Execute(data);
    }

    [Fact]
    public void GenericCommandShouldBeFoundAndExecutedCorrectly()
    {
        // Arrange
        var data = new SimpleData("Hello!");

        var command = new GenericCommand<SimpleData>(data);

        // Act
        _mediator.Send(command);

        // Assert
        _serviceMock.Received(1).Execute(data);
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

    [Fact]
    public async Task ExecuteTwoAsyncGenericCommandHandlersWithDifferentTypesShouldFoundAndExecutedCorrectly()
    {
        // Arrange
        var data = new SimpleData("Hello!");
        var entity = new SomeEntity("SomeEntity Hello!");

        var dataDommand = new GenericCommand<SimpleData>(data);
        var entityCommand = new GenericCommand<SomeEntity>(entity);

        // Act
        await _mediator.SendAsync(dataDommand);
        await _mediator.SendAsync(entityCommand);

        // Assert
        _serviceMock.Received(1).Execute(data);
        _serviceMock.Received(1).Execute(entity);
    }

    [Fact]
    public void ExecuteTwoGenericCommandHandlersWithDifferentTypesShouldPassedFoundAndExecutedCorrectly()
    {
        // Arrange
        var data = new SimpleData("Hello!");
        var entity = new SomeEntity("SomeEntity Hello!");

        var dataDommand = new GenericCommand<SimpleData>(data);
        var entityCommand = new GenericCommand<SomeEntity>(entity);

        // Act
        _mediator.Send(dataDommand);
        _mediator.Send(entityCommand);

        // Assert
        _serviceMock.Received(1).Execute(data);
        _serviceMock.Received(1).Execute(entity);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}