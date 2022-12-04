using System.Threading.Tasks;
using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Handlers
{
    public interface ICommandHandlerAsync<in T> : ICommandHandlerAsync where T : ICommandAsync
    {
        Task HandleAsync(T command);
    }
}