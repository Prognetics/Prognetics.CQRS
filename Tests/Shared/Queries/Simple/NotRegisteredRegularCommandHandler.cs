using System.Threading.Tasks;

namespace Prognetics.CQRS.Tests.Shared.Querys.Simple;

public class NotRegisteredRegularQuery : IQuery<string>
{ }

public class NotRegisteredRegularQueryHandler : IAsyncQueryHandler<NotRegisteredRegularQuery, string>
{
    public Task<string> Handle(NotRegisteredRegularQuery query)
    {
        return Task.FromResult("This should not be handled");
    }
}
