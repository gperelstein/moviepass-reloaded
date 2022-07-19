namespace MPR.Shared.Messaging.Abstractions
{
    public interface IQueueConnectionManager : IDisposable
    {
        bool Connected { get; }

        void Connect();
    }
}
