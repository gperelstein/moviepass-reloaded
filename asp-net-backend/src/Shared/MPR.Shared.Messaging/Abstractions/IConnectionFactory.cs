using RabbitMQ.Client;

namespace MPR.Shared.Messaging.Abstractions
{
    public interface IConnectionFactory
    {
        IConnection CreateConnection();
    }
}
