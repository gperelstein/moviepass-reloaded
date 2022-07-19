using MPR.Shared.Notifications.Messages;

namespace MPR.Notifications.Service.Client
{
    public interface ISmtpProvider
    {
        Task<bool> SendNotificationAsync(IEmailNotification notification);
    }
}
