using Microsoft.Extensions.Options;
using MPR.Notifications.Service.Configuration;
using MPR.Shared.Notifications.Messages;
using System.Net;
using System.Net.Mail;

namespace MPR.Notifications.Service.Client
{
    public class SmtpProvider : ISmtpProvider
    {
        private readonly NotificationServiceOptions _config;

        public SmtpProvider(IOptions<NotificationServiceOptions> options)
        {
            _config = options.Value;
        }

        public async Task<bool> SendNotificationAsync(IEmailNotification notification)
        {
            if (notification == null)
            {
                return false;
            }

            var emailNotification = notification as EmailNotification;

            var message = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(_config.Smtp.FromEmail, _config.Smtp.FromName),
                Subject = emailNotification.Subject,
                Body = emailNotification.ParsedResult
            };

            foreach (var emailAddress in emailNotification.To.Split(';'))
            {
                message.To.Add(new MailAddress(emailAddress));
            }

            using var client = new SmtpClient(_config.Smtp.Host, _config.Smtp.Port);
            client.Credentials = new NetworkCredential(_config.Smtp.SmtpUsername, _config.Smtp.SmtpPassword);
            client.EnableSsl = _config.Smtp.EnableSsl;
            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
