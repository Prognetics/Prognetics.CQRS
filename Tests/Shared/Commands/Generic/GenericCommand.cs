namespace Prognetics.CQRS.Tests.Shared.Commands.Generic;

public class GenericCommand<T> : ICommand
{
    public GenericCommand(T data)
    {
        Data = data;
    }

    public T Data { get; }
}