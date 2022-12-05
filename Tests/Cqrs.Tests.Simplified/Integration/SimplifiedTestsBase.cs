using Autofac;
using Prognetics.CQRS.Simplified.Autofac;
using System.Reflection;

namespace Prognetics.CQRS.Tests.Simplified.Integration;
public abstract class SimplifiedTestsBase
{
    protected IContainer Container { get; }

	public SimplifiedTestsBase()
	{
		var builder = new ContainerBuilder();
		builder.RegisterProgenticsCQRSModule(Assembly.GetExecutingAssembly());
		Container = builder.Build();
	}
}
