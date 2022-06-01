using MPR.Auth.Api;
using MPR.Auth.Data;
using MPR.Auth.Data.SeedData;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    // this seeding is only for the template to bootstrap the DB and users.
    // in production you will likely want a different approach.
    var seed = args.Any(x => x == "/seed");
    var seedUsers = args.Any(x => x == "/seedUsers");
    if (seed || seedUsers)
    {
        args = args
            .Except(new[] { "/seed" })
            .Except(new[] { "/seedUsers" })
            .ToArray();
    }

    if (seed)
    {
        SeedData.SeedIdentityData(app.Services);
        return;
    }
    if (seedUsers)
    {
        await SeedData.SeedUsers(app.Services);
        return;
    }

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}