namespace MPR.Users.Configuration.Configurations
{
    public class QueuesOptions
    {
        public string HostName { get; set; }
        public int HostPort { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ConnectionRetries { get; set; }
        public int WaitBetweenRetriesInSeconds { get; set; }
        public string NotificationQueueName { get; set; }
    }
}
