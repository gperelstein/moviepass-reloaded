using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MPR.Notifications.Service.Client;
using MPR.Notifications.Service.Configuration;
using MPR.Notifications.Service.Parser;
using MPR.Notifications.Service.TemplateLocator;
using MPR.Shared.Messaging.Abstractions;
using MPR.Shared.Messaging.Implementations;
using MPR.Shared.Messaging.Models;
using MPR.Shared.Notifications.Messages;
using MPR.Shared.Notifications.Templates;

namespace MPR.Notifications.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotificationsService(this IServiceCollection services, HostBuilderContext hostContext)
        {
            var config = hostContext.Configuration.GetSection(NotificationServiceOptions.AppConfiguration);
            var appConfigurations = new NotificationServiceOptions();
            config.Bind(appConfigurations);
            services.Configure<NotificationServiceOptions>(config);

            services.AddSingleton<NotificationService>();
            services.AddTransient<IParser, DefaultParser>();
            services.AddTransient<ISmtpProvider, SmtpProvider>();
            services.AddTransient<ITemplateLocator, DefaultTemplateLocator>();
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddSingleton<IConsumerFactory, ConsumerFactory>();
            services.AddSingleton<IQueueConnectionManager, RabbitMqConnectionManager>();
            services.AddHostedService<NotificationService>();
            services.AddSingleton(new QueueConnectionSettings
            {
                Host = appConfigurations.Queues.HostName,
                Port = appConfigurations.Queues.HostPort,
                VirtualHost = appConfigurations.Queues.VirtualHost,
                UserName = appConfigurations.Queues.UserName,
                Password = appConfigurations.Queues.Password,
                ConnectionRetries = appConfigurations.Queues.ConnectionRetries,
                WaitBetweenRetries = TimeSpan.FromSeconds(appConfigurations.Queues.WaitBetweenRetriesInSeconds),
                Routes = new List<RoutingSettings>
                {
                    RouteSettingsGenerator.CreateConsumer<NotificationService, UserRegistration>()
                        .ConnectedToQueue(appConfigurations.Queues.NotificationQueueName)
                        .WithPrefetch(appConfigurations.Queues.UsePrefetch, appConfigurations.Queues.PrefetchCount)
                        .Build()
                }
            });
            return services;
        }
    }
}
