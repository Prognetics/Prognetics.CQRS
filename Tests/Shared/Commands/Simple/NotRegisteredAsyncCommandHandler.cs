namespace Prognetics.CQRS.Tests.Shared.Commands.Simple;

public class NotRegisteredAsyncCommand : ICommand
{ }

public class NotRegisteredAsyncCommandHandler : ICommandHandler<NotRegisteredAsyncCommand>
{
    private readonly IServiceForTests _serviceForTests;

    public NotRegisteredAsyncCommandHandler(IServiceForTests serviceForTests)
    {
        _serviceForTests = serviceForTests;
    }

    public void Handle(NotRegisteredAsyncCommand command)
    {
        _serviceForTests.Execute(command);
    }
}
