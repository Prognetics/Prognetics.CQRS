using Prognetics.CQRS.Simplified;

namespace Prognetics.CQRS.Tests.Simplified.Integration.Event
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