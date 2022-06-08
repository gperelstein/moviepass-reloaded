using MPR.Shared.Notifications.Messages;
using System.Reflection;

namespace MPR.Notifications.Service.TemplateLocator
{
    public class DefaultTemplateLocator : ITemplateLocator
    {
        public async Task<string> LocateTemplateAsync(IEmailNotification notification)
        {
            var resourceAssembly = Assembly.GetExecutingAssembly();
            var resourcesName = resourceAssembly.GetManifestResourceNames();
            var resourceName = resourcesName.FirstOrDefault(x => x.EndsWith(notification.TemplateName));

            if (resourceName == null)
            {
                return null;
            }

            Stream stream = null;

            try
            {
                stream = resourceAssembly.GetManifestResourceStream(resourceName);
                using var streamReader = new StreamReader(stream);
                stream = null;
                return await streamReader.ReadToEndAsync();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
        }
    }
}
