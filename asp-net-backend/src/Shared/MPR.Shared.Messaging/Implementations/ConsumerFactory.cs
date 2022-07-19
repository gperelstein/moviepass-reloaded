using MPR.Shared.Messaging.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MPR.Shared.Messaging.Implementations
{
    public class ConsumerFactory : IConsumerFactory
    {
        public EventingBasicConsumer GetNewEventConsumer(IModel channel)
        {
            return new EventingBasicConsumer(channel);
        }
    }
}
