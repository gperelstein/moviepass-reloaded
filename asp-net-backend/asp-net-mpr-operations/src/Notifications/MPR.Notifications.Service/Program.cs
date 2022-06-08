using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MPR.Notifications.Service;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddNotificationsService(hostContext);
    })
    .ConfigureLogging((hostContext, configLogging) =>
    {
        configLogging.AddConsole();

    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();
