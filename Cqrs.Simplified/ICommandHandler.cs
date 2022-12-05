using System.Threading.Tasks;

namespace Prognetics.CQRS.Simplified;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    void Handle(TCommand command);
}

public interface IAsyncCommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command);
}