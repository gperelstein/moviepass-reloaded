using MPR.Shared.Notifications.Messages;

namespace MPR.Notifications.Service.Parser
{
    public interface IParser
    {
        string Parse(string template, IEmailNotification notification);
    }
}
