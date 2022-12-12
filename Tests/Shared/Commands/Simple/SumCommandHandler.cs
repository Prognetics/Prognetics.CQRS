using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Shared.Commands.Simple;

public class SumCommandHandler :
    ICommandHandler<SumCommand>,
    IAsyncCommandHandler<SumCommand>
{
    private readonly IServiceForTests _service;

    public SumCommandHandler(IServiceForTests service)
    {
        _service = service;
    }

    public void Handle(SumCommand command)
    {
        var result = command.First + command.Second;
        _service.Execute(result);
    }

    Task IAsyncCommandHandler<SumCommand>.Handle(SumCommand command)
    {
        var result = command.First + command.Second;
        _service.Execute(result);
        return Task.CompletedTask;
    }
}