using System.Threading.Tasks;

namespace Prognetics.CQRS;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand query);
}
