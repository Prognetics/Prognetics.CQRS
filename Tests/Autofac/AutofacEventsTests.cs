using Autofac;
using NSubstitute;
using Prognetics.CQRS.Tests.Integration.Event;
using Prognetics.CQRS.Tests.Shared;
using System.Threading.Tasks;
using Xunit;

namespace Prognetics.CQRS.Tests.Autofac;
public class AutofacEventsTests : IClassFixture<AutofacFixture>
{
    private readonly ILifetimeScope _scope;
    private readonly IMediator _mediator;
    private readonly IServiceForTests _serviceMock;

    public AutofacEventsTests(AutofacFixture fixture)
    {
        _scope = fixture.Container.BeginLifetimeScope();
        _mediator = _scope.Resolve<IMediator>();
        _serviceMock = _scope.Resolve<IServiceForTests>();
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
