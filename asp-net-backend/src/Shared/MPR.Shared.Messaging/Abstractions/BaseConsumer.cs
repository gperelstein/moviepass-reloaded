namespace MPR.Shared.Messaging.Abstractions
{
    public abstract class BaseConsumer<T> : IConsumer where T : BaseMessage
    {
        public Task OnMessage(object message)
        {
            return OnMessage(message as T);
        }
        public abstract Task OnMessage(T message);
        public abstract void OnMessageDeserializationError(Exception ex, string rawMessage);
    }

    public interface IConsumer
    {
        Task OnMessage(object message);
        void OnMessageDeserializationError(Exception ex, string rawMessage);
    }
}
