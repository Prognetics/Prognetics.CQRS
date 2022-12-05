using System.Threading.Tasks;
using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Handlers
{
    public abstract class GenericCommandHandlerAsync<T, TR> : IGenericCommandHandlerAsync<T> where T : IGenericCommandAsync<TR> where TR : class
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task HandleAsync(T command)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        { }
    }
}