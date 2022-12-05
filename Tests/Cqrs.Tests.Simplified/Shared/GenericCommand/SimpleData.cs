namespace Prognetics.CQRS.Simplified.Tests.Shared.GenericCommand
{
    public class SimpleData
    {
        public SimpleData(string data)
        {
            Data = data;
        }

        public string Data { get; }
    }
}