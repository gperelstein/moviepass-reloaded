using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MPR.Shared.Messaging.Abstractions
{
    public interface IConsumerFactory
    {
        EventingBasicConsumer GetNewEventConsumer(IModel channel);
    }
}
