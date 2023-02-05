using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Shared.Commands.Simple;

public class NotRegisteredRegularCommand : ICommand
{ }

public class NotRegisteredRegularCommandHandler : IAsyncCommandHandler<NotRegisteredRegularCommand>
{
    private readonly IServiceForTests _serviceForTests;

    public NotRegisteredRegularCommandHandler(IServiceForTests serviceForTests)
    {
        _serviceForTests = serviceForTests;
    }

    public Task Handle(NotRegisteredRegularCommand command)
    {
        _serviceForTests.Execute(command);
        return Task.CompletedTask;
    }
}
