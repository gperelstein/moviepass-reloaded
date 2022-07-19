using MPR.Shared.Messaging.Abstractions;

namespace MPR.Shared.Messaging.Models
{
    public class RouteSettingsGenerator
    {
        private readonly RoutingSettings _settings;

        public RouteSettingsGenerator(Type routerClassType, Type messageType, string routeType)
        {
            _settings = new RoutingSettings
            {
                RouterClassType = routerClassType,
                MessageType = messageType,
                RouteType = routeType,
                ExchangeKey = string.Empty,
                DynamicRoute = false,
                AutoAck = true
            };
        }

        public static RouteSettingsGenerator CreateProducer<TProducer, TMessage>() where TProducer : IProducer where TMessage : BaseMessage
        {
            return new RouteSettingsGenerator(typeof(TProducer), typeof(TMessage), RouteTypes.OUTBOUND_ROUTE);
        }

        public static RouteSettingsGenerator CreateConsumer<TConsumer, TMessage>() where TConsumer : IConsumer where TMessage : BaseMessage
        {
            return new RouteSettingsGenerator(typeof(TConsumer), typeof(TMessage), RouteTypes.INBOUND_ROUTE);
        }

        public RouteSettingsGenerator WithPrefetch(bool prefetch, ushort prefetchCount)
        {
            if (!prefetch)
            {
                return this;
            }
            _settings.AutoAck = false;
            _settings.PrefetchCount = prefetchCount;
            return this;
        }

        public RouteSettingsGenerator WithExchangeKey(string exchangeKey)
        {
            _settings.ExchangeKey = exchangeKey;
            return this;
        }

        public RouteSettingsGenerator ConnectedToQueue(string queueName)
        {
            _settings.QueueName = queueName;
            return this;
        }

        public RouteSettingsGenerator UsingDynamicRoute()
        {
            _settings.DynamicRoute = true;
            return this;
        }

        public RoutingSettings Build()
        {
            Validate();
            return _settings;
        }

        private void Validate()
        {
            if (_settings.RouterClassType == null
                || (!_settings.RouterClassType.GetInterfaces().Contains(typeof(IConsumer))
                && !_settings.RouterClassType.GetInterfaces().Contains(typeof(IProducer))))
            {
                throw new Exception("Unable to build route settings. Missing or invalid RouterClassType.");
            }
            if (_settings.MessageType == null || !_settings.MessageType.IsSubclassOf(typeof(BaseMessage)))
            {
                throw new Exception("Unable to build route settings. Missing or invalid MessageType.");
            }
            if (!_settings.DynamicRoute && string.IsNullOrWhiteSpace(_settings.QueueName))
            {
                throw new Exception("Unable to build route settings. Missing or invalid QueueName. Are you missing the call to ConnectedToQueue method?");
            }
            if (_settings.DynamicRoute && string.IsNullOrWhiteSpace(_settings.ExchangeKey))
            {
                throw new Exception("Unable to build route settings. Missing or invalid ExchangeKey. Are you missing the call to WithExchangeKey method?");
            }
        }
    }
}
