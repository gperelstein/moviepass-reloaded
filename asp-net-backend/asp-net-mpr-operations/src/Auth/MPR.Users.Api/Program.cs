using MPR.Auth.Data;
using MPR.Users.Api;
using MPR.Users.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddUsersPresentation();
builder.Services.AddUsersLogic();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
