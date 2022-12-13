using Autofac;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Prognetics.CQRS.Tests.Integration.Event;
using Prognetics.CQRS.Tests.Shared;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Prognetics.CQRS.Tests.Autofac;
public class MicrosoftDIEventsTests : IClassFixture<MicrosoftDIFixture>
{
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;
    private readonly IServiceForTests _serviceMock;

    public MicrosoftDIEventsTests(MicrosoftDIFixture fixture)
    {
        _scope = fixture.ServiceProvider.CreateScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        _serviceMock = _scope.ServiceProvider.GetRequiredService<IServiceForTests>();
    }

    [Fact]
    public async Task WhenFireTheEvent_ShouldBeHandledTwoTimesProperly()
    {
        //Act
        await _mediator.Publish(new NumbersEvent(2, 7));

        //Assert
        _serviceMock.Received(1).Execute(9);
        _serviceMock.Received(1).Execute(-5);
    }
}
