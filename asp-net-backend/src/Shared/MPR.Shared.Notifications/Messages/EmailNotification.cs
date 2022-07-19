using MPR.Shared.Messaging.Abstractions;

namespace MPR.Shared.Notifications.Messages
{
    public abstract class EmailNotification : BaseMessage, IEmailNotification
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string ParsedResult { get; set; }
        public abstract string TemplateName { get; }
    }
}
