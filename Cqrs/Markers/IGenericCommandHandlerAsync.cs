using System.Threading.Tasks;

namespace Prognetics.CQRS.Markers
{
    public interface IGenericCommandHandlerAsync<in T>
    {
        Task HandleAsync(T command);
    }
}