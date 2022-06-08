using MPR.Shared.Messaging.Abstractions;
using MPR.Shared.Messaging.Models;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MPR.Shared.Messaging.Implementations
{
    public sealed class RabbitMqConnectionManager : IQueueConnectionManager
    {
        private readonly QueueConnectionSettings _settings;
        private readonly Abstractions.IConnectionFactory _factory;
        private readonly IConsumerFactory _consumerFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, IModel> _channels;
        private readonly Dictionary<string, AsyncEventingBasicConsumer> _consumers;
        private IConnection _connection;

        public RabbitMqConnectionManager(QueueConnectionSettings settings, Abstractions.IConnectionFactory factory, IConsumerFactory consumerFactory, IServiceProvider serviceProvider)
        {
            _settings = settings;
            _factory = factory;
            _consumerFactory = consumerFactory;
            _serviceProvider = serviceProvider;
            _channels = new Dictionary<string, IModel>();
            _consumers = new Dictionary<string, AsyncEventingBasicConsumer>();
        }

        public bool Connected { get; private set; }

        public void Connect()
        {
            Policy.Handle<Exception>().WaitAndRetry(
                retryCount: _settings.ConnectionRetries,
                sleepDurationProvider: i => _settings.WaitBetweenRetries
            ).Execute(() =>
            {
                _connection = _factory.CreateConnection();
                Connected = true;
            });
            CreateProducers();
            CreateConsumers();
        }

        private void CreateConsumers()
        {
            if (_settings.Routes == null || _settings.Routes.Count == 0)
            {
                throw new QueueConnectionException("No Routes defined");
            }

            var serializationSettings = _settings.SerializationSettings ?? new JsonSerializerSettings();

            foreach (var route in _settings.Routes.Where(r => r.RouteType.Equals(RouteTypes.INBOUND_ROUTE)))
            {
                if (_consumers.ContainsKey(route.IdentificationKey))
                {
                    throw new QueueConnectionException($"A consumer for the message type '{route.MessageType.Name}' was already registered, unable to register consumer '{route.RouterClassType.Name}'");
                }

                var consumer = _serviceProvider.GetService(route.RouterClassType) as IConsumer;
                if (consumer == null)
                {
                    throw new QueueConnectionException($"Unable to get an instance for the consumer type '{route.RouterClassType.Name}'");
                }

                var channel = GetChannel(route, true);
                object obj;
                var asyncConsumer = new AsyncEventingBasicConsumer(channel);

                asyncConsumer.Received += async (_, ea) =>
                {
                    if (!route.AutoAck && !channel.IsClosed)
                    {
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }

                    string rawMessage = string.Empty;
                    try
                    {
                        var body = ea.Body.ToArray();
                        rawMessage = Encoding.UTF8.GetString(body);
                        obj = JsonConvert.DeserializeObject(rawMessage, route.MessageType, serializationSettings);
                    }
                    catch (Exception ex)
                    {
                        consumer.OnMessageDeserializationError(ex, rawMessage);
                        return;
                    }
                    await consumer.OnMessage(obj);
                };
                channel.BasicConsume(route.QueueName, route.AutoAck, asyncConsumer);
                _consumers.Add(route.IdentificationKey, asyncConsumer);
            }
        }


        private void CreateProducers()
        {
            if (_settings.Routes == null || _settings.Routes.Count == 0)
            {
                throw new QueueConnectionException("No Routes defined");
            }

            var serializationSettings = _settings.SerializationSettings ?? new JsonSerializerSettings();

            foreach (var route in _settings.Routes.Where(r => r.RouteType.Equals(RouteTypes.OUTBOUND_ROUTE)))
            {
                var producer = _serviceProvider.GetService(route.RouterClassType) as IProducer;
                if (producer == null)
                {
                    throw new QueueConnectionException($"Unable to get an instance for the producer type '{route.RouterClassType.Name}'");
                }
                var channel = GetChannel(route, false);
                producer.OnMessageProduced += (msg, optionalRoutingKey) =>
                {
                    channel.BasicPublish(route.ExchangeKey, route.DynamicRoute ? optionalRoutingKey : route.QueueName, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg, serializationSettings)));
                };
            }
        }

        private IModel GetChannel(RoutingSettings route, bool forConsumer)
        {
            var channelId = GetChannelIdentification(route, forConsumer);
            if (_channels.ContainsKey(channelId))
            {
                return _channels[channelId];
            }

            var channel = _connection.CreateModel();
            if (route.PrefetchCount != 0)
            {
                channel.BasicQos(0, route.PrefetchCount, false);
            }
            if (route.DynamicRoute)
            {
                channel.ExchangeDeclare(exchange: route.ExchangeKey, type: "direct", durable: true, autoDelete: false, arguments: null);
            }

            if (!string.IsNullOrEmpty(route.QueueName))
            {
                channel.QueueDeclare(queue: route.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                if (route.DynamicRoute)
                {
                    channel.QueueBind(queue: route.QueueName, exchange: route.ExchangeKey, routingKey: route.QueueName);
                }
            }
            _channels.Add(channelId, channel);
            return channel;
        }

        private string GetChannelIdentification(RoutingSettings route, bool forConsumer)
        {
            return forConsumer
                ? "CONSUMER_" + route.IdentificationKey
                : "SENDER_" + route.QueueName;
        }

        public void Dispose()
        {
            _channels.Values.ToList().ForEach(c => c.Dispose());
            _channels.Clear();
            _consumers.Clear();
            Connected = false;
            _connection?.Dispose();
        }
    }
}
