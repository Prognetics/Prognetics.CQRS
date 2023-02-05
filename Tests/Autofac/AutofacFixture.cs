using Autofac;
using NSubstitute;
using Prognetics.CQRS.Autofac;
using Prognetics.CQRS.Tests.Shared;
using System;
using System.Reflection;

namespace Prognetics.CQRS.Tests.Autofac;

public class AutofacFixture : IDisposable
{
    public IContainer Container { get; }

    public AutofacFixture()
    {
        var builder = new ContainerBuilder();
        builder.RegisterProgenticsCQRSModule(Assembly.GetExecutingAssembly());
        builder.Register(_ => Substitute.For<IServiceForTests>()).InstancePerLifetimeScope();
        Container = builder.Build();
    }

    public void Dispose()
    {
        Container.Dispose();
    }
}
