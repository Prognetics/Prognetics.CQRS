using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Benchmarks;

internal class BenchmarkSimplifiedGenericCommand<T> : ICommand
{
    public BenchmarkSimplifiedGenericCommand(T data)
    {
        Data = data;
    }

    public T Data { get; }
}

internal class BenchmarkSimplifiedGenericAsyncCommandHandler<T> : IAsyncCommandHandler<BenchmarkSimplifiedGenericCommand<T>>
{
    public Task Handle(BenchmarkSimplifiedGenericCommand<T> command)
    {
        for (int i = 0; i < 1e6; i++)
        {
            var result = Convert.ToInt32(command.Data?.ToString());
        }
        return Task.CompletedTask;
    }
}
