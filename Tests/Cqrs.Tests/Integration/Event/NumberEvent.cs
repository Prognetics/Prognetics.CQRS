using Prognetics.CQRS.Markers;

namespace Prognetics.CQRS.Tests.Integration.Event
{
    public class NumberEvent : IEventAsync
    {
        public NumberEvent(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}