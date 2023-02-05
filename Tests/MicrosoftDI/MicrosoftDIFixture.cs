using Autofac;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Prognetics.CQRS.MicrosoftDI;
using Prognetics.CQRS.Tests.Shared;
using System;
using System.Reflection;

namespace Prognetics.CQRS.Tests.Autofac;

public class MicrosoftDIFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; }

    public MicrosoftDIFixture()
    {
        var builder = new ServiceCollection();
        builder.AddProgneticsCQRS(Assembly.GetExecutingAssembly());
        builder.AddScoped(_ => Substitute.For<IServiceForTests>());
        ServiceProvider = builder.BuildServiceProvider();
    }

    public void Dispose()
    { }
}
