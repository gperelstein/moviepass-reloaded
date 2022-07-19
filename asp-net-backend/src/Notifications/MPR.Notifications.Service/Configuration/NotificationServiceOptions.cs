namespace MPR.Notifications.Service.Configuration
{
    public class NotificationServiceOptions
    {
        public const string AppConfiguration = "AppConfiguration";
        public SmtpOptions Smtp { get; set; }
        public QueuesOptions Queues { get; set; }
    }
}
