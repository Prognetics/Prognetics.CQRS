using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Shared.Commands.Generic;

public class AsyncGenericCommandHandler<T> : IAsyncCommandHandler<GenericCommand<T>>
{
    private readonly IServiceForTests _service;

    public AsyncGenericCommandHandler(IServiceForTests service)
    {
        _service = service;
    }

    public Task Handle(GenericCommand<T> command)
    {
        _service.Execute(command.Data);
        return Task.CompletedTask;
    }
}
