using Prognetics.CQRS.Tests.Shared;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Integration.Event;

public class DiffNumberEventHandler : IEventHandler<NumbersEvent>
{
    private readonly IServiceForTests _serviceForTests;

    public DiffNumberEventHandler(IServiceForTests serviceForTests)
    {
        _serviceForTests = serviceForTests;
    }

    public Task Handle(NumbersEvent @event)
    {
        _serviceForTests.Execute(@event.First - @event.Second);
        return Task.CompletedTask;
    }
}