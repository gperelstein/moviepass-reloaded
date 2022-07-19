using MPR.Auth.Data;
using MPR.Shared.Messaging.Abstractions;
using MPR.Users.Api;
using MPR.Users.Configuration;
using MPR.Users.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration.GetSection(UsersServiceOptions.AppConfiguration);
var appConfigurations = new UsersServiceOptions();
config.Bind(appConfigurations);
builder.Services.Configure<UsersServiceOptions>(config);

builder.Services.AddUsersPresentation();
builder.Services.AddUsersLogic(appConfigurations);
builder.Services.AddAuthData(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

var applicationLifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
var queueConnectionManager = app.Services.GetRequiredService<IQueueConnectionManager>();
queueConnectionManager.Connect();

applicationLifetime.ApplicationStopping.Register(() =>
{
    queueConnectionManager?.Dispose();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
