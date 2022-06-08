using MPR.Shared.Notifications.Messages;

namespace MPR.Notifications.Service.TemplateLocator
{
    public interface ITemplateLocator
    {
        Task<string> LocateTemplateAsync(IEmailNotification notification);
    }
}
