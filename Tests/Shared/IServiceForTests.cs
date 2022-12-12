namespace Prognetics.CQRS.Tests.Shared;
public interface IServiceForTests
{
    void Execute<T>(T data);
}
