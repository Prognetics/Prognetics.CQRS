using System.Threading.Tasks;
using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Handlers
{
    public class CommandHandlerAsync<T> : ICommandHandlerAsync<T> where T : ICommandAsync
    {
#pragma warning disable 1998
        public virtual async Task HandleAsync(T command)
#pragma warning restore 1998
        { }
    }
}