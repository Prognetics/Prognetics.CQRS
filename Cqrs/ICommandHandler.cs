using System.Threading.Tasks;

namespace Prognetics.CQRS;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    void Handle(TCommand command);
}

public interface IAsyncCommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command);
}