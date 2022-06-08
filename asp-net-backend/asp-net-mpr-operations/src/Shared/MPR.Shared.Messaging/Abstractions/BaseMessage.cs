namespace MPR.Shared.Messaging.Abstractions
{
    public abstract class BaseMessage
    {
        protected BaseMessage()
        {
            Timestamp = DateTimeOffset.Now;
        }

        public Guid CorrelationId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
