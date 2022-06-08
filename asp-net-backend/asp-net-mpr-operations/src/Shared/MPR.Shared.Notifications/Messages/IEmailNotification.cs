namespace MPR.Shared.Notifications.Messages
{
    public interface IEmailNotification
    {
        public string TemplateName { get; }
        public string ParsedResult { get; set; }
    }
}
