namespace MPR.Shared.Messaging.Abstractions
{
    public delegate void MessageProduced(object msg, string optionalRoutingKey = null);

    public abstract class BaseProducer<T> : IProducer where T : BaseMessage
    {
        public event MessageProduced OnMessageProduced;

        public virtual void SendMessage(T message, string optionalRoutingKey = null)
        {
            OnMessageProduced?.Invoke(message, optionalRoutingKey);
        }
    }

    public interface IProducer
    {
        event MessageProduced OnMessageProduced;
    }
}
