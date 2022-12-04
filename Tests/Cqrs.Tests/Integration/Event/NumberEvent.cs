namespace Prognetics.CQRS.Tests.Integration.Event
{
    public class NumberEvent : IEvent
    {
        public NumberEvent(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}