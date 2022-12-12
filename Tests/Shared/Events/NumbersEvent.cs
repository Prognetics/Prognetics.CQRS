namespace Prognetics.CQRS.Tests.Integration.Event;

public class NumbersEvent : IEvent
{
    public NumbersEvent(
        int first,
        int second)
    {
        First = first;
        Second = second;
    }

    public int First { get; }
    public int Second { get; }
}