using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Benchmarks;

internal class BenchmarkRegularGenericAsyncCommand<T> : IGenericCommandAsync<T>
{
    public BenchmarkRegularGenericAsyncCommand(T data)
    {
        Data = data;
    }

    public T Data { get; }
}

internal class BenchmarkRegularGenericCommandHandler<T> :
    IGenericCommandHandlerAsync<BenchmarkRegularGenericAsyncCommand<T>>
{
    public Task HandleAsync(BenchmarkRegularGenericAsyncCommand<T> command)
    {
        for (int i = 0; i < 1e6; i++)
        {
            var result = Convert.ToInt32(command.Data?.ToString());
        }
        return Task.CompletedTask;
    }
}
