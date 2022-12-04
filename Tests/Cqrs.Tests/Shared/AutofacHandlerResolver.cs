using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Shared;
internal class AutofacHandlerResolver : IHandlerResolver
{
	private readonly IComponentContext _context;

	public AutofacHandlerResolver(IComponentContext context)
	{
		_context = context;
	}

	public T Resolve<T>()
		=> _context.Resolve<T>();
}
