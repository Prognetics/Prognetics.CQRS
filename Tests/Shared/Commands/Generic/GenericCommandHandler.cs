namespace Prognetics.CQRS.Tests.Shared.Commands.Generic;

public class GenericCommandHandler<T> : ICommandHandler<GenericCommand<T>>
{
    private readonly IServiceForTests _service;

    public GenericCommandHandler(IServiceForTests service)
    {
        _service = service;
    }

    public void Handle(GenericCommand<T> command)
    {
        _service.Execute(command.Data);
    }
}

public class StringGenericCommandHandler : ICommandHandler<GenericCommand<string>>
{
    private readonly IServiceForTests _service;

    public StringGenericCommandHandler(IServiceForTests service)
    {
        _service = service;
    }

    public void Handle(GenericCommand<string> command)
    {
        _service.Execute(command.Data.ToUpper());
    }
}