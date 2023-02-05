namespace Prognetics.CQRS.Tests.Shared.Querys.Simple;

public class NotRegisteredAsyncQuery : IQuery<string>
{ }

public class NotRegisteredAsyncQueryHandler : IQueryHandler<NotRegisteredAsyncQuery, string>
{
    public string Handle(NotRegisteredAsyncQuery query)
    {
        return "This shouldnt be handled";
    }
}
