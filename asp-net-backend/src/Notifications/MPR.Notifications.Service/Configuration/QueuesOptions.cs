namespace MPR.Notifications.Service.Configuration
{
    public class QueuesOptions
    {
        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int HostPort { get; set; }
        public bool UsePrefetch { get; set; }
        public ushort PrefetchCount { get; set; }
        public int ConnectionRetries { get; set; }
        public int WaitBetweenRetriesInSeconds { get; set; }
        public string NotificationQueueName { get; set; }
    }
}
