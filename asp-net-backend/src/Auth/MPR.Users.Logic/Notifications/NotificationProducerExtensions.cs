using MPR.Shared.Messaging.Models;
using MPR.Shared.Notifications.Messages;
using MPR.Shared.Notifications.Templates;

namespace MPR.Users.Logic.Notifications
{
    public static class NotificationProducerExtensions
    {
        public static RoutingSettings CreateNotificationsProducerRouteSettings(string queueName)
        {
            return RouteSettingsGenerator.CreateProducer<NotificationsProducer, UserRegistration>()
                .ConnectedToQueue(queueName)
                .Build();
        }
    }
}
