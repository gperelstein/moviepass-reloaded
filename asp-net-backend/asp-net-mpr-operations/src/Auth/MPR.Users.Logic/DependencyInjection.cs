using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using MPR.Shared.Logic;
using MPR.Shared.Messaging.Abstractions;
using MPR.Shared.Messaging.Implementations;
using MPR.Shared.Messaging.Models;
using MPR.Users.Configuration;
using MPR.Users.Logic.Notifications;

namespace MPR.Users.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersLogic(this IServiceCollection services, UsersServiceOptions config)
        {
            services.AddSharedLogic("MPR.Users.Logic");
            services.AddSingleton<Shared.Messaging.Abstractions.IConnectionFactory, ConnectionFactory>();
            services.AddSingleton<IConsumerFactory, ConsumerFactory>();
            services.AddSingleton<IQueueConnectionManager, RabbitMqConnectionManager>();
            services.AddSingleton(new QueueConnectionSettings
            {
                Host = config.Queues.HostName,
                Port = config.Queues.HostPort,
                VirtualHost = config.Queues.VirtualHost,
                UserName = config.Queues.UserName,
                Password = config.Queues.Password,
                ConnectionRetries = config.Queues.ConnectionRetries,
                WaitBetweenRetries = TimeSpan.FromSeconds(config.Queues.WaitBetweenRetriesInSeconds),
                Routes = new List<RoutingSettings>
                {
                    NotificationProducerExtensions.CreateNotificationsProducerRouteSettings(config.Queues.NotificationQueueName)
                }
            });
            services.AddSingleton<NotificationsProducer>();
            return services;
        }
    }
}
