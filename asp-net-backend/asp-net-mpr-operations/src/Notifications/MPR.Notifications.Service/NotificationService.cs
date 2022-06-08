using Microsoft.Extensions.Hosting;
using MPR.Notifications.Service.Client;
using MPR.Notifications.Service.Parser;
using MPR.Notifications.Service.TemplateLocator;
using MPR.Shared.Messaging.Abstractions;
using MPR.Shared.Notifications.Messages;
using MPR.Shared.Notifications.Templates;

namespace MPR.Notifications.Service
{
    public class NotificationService : BaseConsumer<UserRegistration>, IHostedService
    {
        private readonly ITemplateLocator _locator;
        private readonly IParser _parser;
        private readonly ISmtpProvider _provider;
        private readonly IQueueConnectionManager _queueManager;

        public NotificationService(ITemplateLocator locator,
            IParser parser,
            ISmtpProvider provider,
            IQueueConnectionManager queueManager)
        {
            _locator = locator;
            _parser = parser;
            _provider = provider;
            _queueManager = queueManager;
        }

        public override async Task OnMessage(UserRegistration message)
        {
            if (message == null)
            {
                return;
            }

            var template = await _locator.LocateTemplateAsync(message);

            if (template == null)
            {
                return;
            }

            message.ParsedResult = _parser.Parse(template, message);

            await _provider.SendNotificationAsync(message);
        }

        public override void OnMessageDeserializationError(Exception ex, string rawMessage)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _queueManager.Connect();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _queueManager?.Dispose();
            return Task.CompletedTask;
        }
    }
}
