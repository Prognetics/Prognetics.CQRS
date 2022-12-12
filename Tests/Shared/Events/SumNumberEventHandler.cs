using Prognetics.CQRS.Tests.Shared;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Integration.Event;

public class SumNumberEventHandler : IEventHandler<NumbersEvent>
{
    private readonly IServiceForTests _serviceForTests;

    public SumNumberEventHandler(IServiceForTests serviceForTests)
    {
        _serviceForTests = serviceForTests;
    }
    public Task Handle(NumbersEvent @event)
    {
        _serviceForTests.Execute(@event.First + @event.Second);
        return Task.CompletedTask;
    }
}