using MPR.Shared.Messaging.Models;
using RabbitMQ.Client;

namespace MPR.Shared.Messaging.Implementations
{
    public class ConnectionFactory : Abstractions.IConnectionFactory
    {
        private readonly RabbitMQ.Client.ConnectionFactory _factory;

        public ConnectionFactory(QueueConnectionSettings settings)
        {
            _factory = new RabbitMQ.Client.ConnectionFactory
            {
                HostName = settings.Host,
                Port = settings.Port,
                VirtualHost = settings.VirtualHost,
                UserName = settings.UserName,
                Password = settings.Password,
                DispatchConsumersAsync = true
            };
        }

        public IConnection CreateConnection()
        {
            return _factory.CreateConnection();
        }
    }
}
