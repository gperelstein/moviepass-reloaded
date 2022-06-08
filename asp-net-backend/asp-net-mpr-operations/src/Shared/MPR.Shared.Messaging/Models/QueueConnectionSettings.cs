using Newtonsoft.Json;
using System.Text.Json;

namespace MPR.Shared.Messaging.Models
{
    public class QueueConnectionSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ConnectionRetries { get; set; }
        public TimeSpan WaitBetweenRetries { get; set; }
        public List<RoutingSettings> Routes { get; set; }
        public JsonSerializerSettings SerializationSettings { get; set; }
    }

    public class RoutingSettings
    {
        public string RouteType { get; set; }
        public Type RouterClassType { get; set; }
        public Type MessageType { get; set; }
        public string QueueName { get; set; }
        public string ExchangeKey { get; set; }
        public bool DynamicRoute { get; set; }
        public bool AutoAck { get; set; }
        public ushort PrefetchCount { get; set; }

        public string IdentificationKey => MessageType.FullName + "." + QueueName;
    }

    public static class RouteTypes
    {
        public const string INBOUND_ROUTE = "INBOUND_ROUTE";
        public const string OUTBOUND_ROUTE = "OUTBOUND_ROUTE";
    }
}
