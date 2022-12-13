namespace Prognetics.CQRS.Tests.Shared.Commands.Simple;

public record SumCommand(int First, int Second) : ICommand;