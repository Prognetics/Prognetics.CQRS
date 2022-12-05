using Autofac;
using BenchmarkDotNet.Attributes;
using Prognetics.CQRS.Simplified.Autofac;
using Prognetics.CQRS.Tests.Shared.Modules;
using System.Reflection;

namespace Prognetics.CQRS.Benchmarks;

[MemoryDiagnoser]
public class MediatorBenchmarks
{
    private readonly IContainer _simplifiedContainer;
    private readonly IContainer _regularContainer;

    public MediatorBenchmarks()
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterModule(new CqrsModule(Assembly.GetExecutingAssembly().GetName().Name));
        _regularContainer = containerBuilder.Build();

        containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterProgenticsCQRSModule(Assembly.GetExecutingAssembly());
        _simplifiedContainer = containerBuilder.Build();
    }

    [Benchmark]
    public void GenericQuery_Regular()
    {
        using var scope = _regularContainer.BeginLifetimeScope();
        var mediator = scope.Resolve<Mediator.IMediator>();

        var queryOne = new BenchmarkRegularGenericQuery<int>(2);
        var resultOne = mediator.FetchGeneric<BenchmarkRegularGenericQuery<int>, int, int>(queryOne);

        var queryTwo = new BenchmarkRegularGenericQuery<string>("7");
        var resultTwo = mediator.FetchGeneric<BenchmarkRegularGenericQuery<string>, int, string>(queryTwo);
    }

    [Benchmark]
    public void GenericQuery_Simplified()
    {
        using var scope = _simplifiedContainer.BeginLifetimeScope();
        var mediator = scope.Resolve<Simplified.IMediator>();

        var queryOne = new BenchmarkSimplifiedGenericQuery<int>(2);
        var resultOne = mediator.Fetch<BenchmarkSimplifiedGenericQuery<int>, int>(queryOne);

        var queryTwo = new BenchmarkSimplifiedGenericQuery<string>("7");
        var resultTwo = mediator.Fetch<BenchmarkSimplifiedGenericQuery<string>, int>(queryTwo);
    }


    [Benchmark]
    public async Task GenericCommand_Regular()
    {
        using var scope = _regularContainer.BeginLifetimeScope();
        var mediator = scope.Resolve<Mediator.IMediator>();

        var commandOne = new BenchmarkRegularGenericAsyncCommand<int>(2);
        await mediator.SendGenericAsync<BenchmarkRegularGenericAsyncCommand<int>, int>(commandOne);

        var commandTwo = new BenchmarkRegularGenericAsyncCommand<string>("7");
        await mediator.SendGenericAsync<BenchmarkRegularGenericAsyncCommand<string>, string>(commandTwo);
    }

    [Benchmark]
    public async Task GenericCommand_Simplified()
    {
        using var scope = _simplifiedContainer.BeginLifetimeScope();
        var mediator = scope.Resolve<Simplified.IMediator>();

        var commandOne = new BenchmarkSimplifiedGenericCommand<int>(2);
        await mediator.SendAsync(commandOne);

        var commandTwo = new BenchmarkSimplifiedGenericCommand<string>("7");
        await mediator.SendAsync(commandTwo);
    }
}