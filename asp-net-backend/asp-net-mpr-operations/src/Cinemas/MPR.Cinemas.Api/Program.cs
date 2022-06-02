using MPR.Cinemas.Api;
using MPR.Cinemas.Data;
using MPR.Cinemas.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCinemasPresentation();
builder.Services.AddCinemasLogic();
builder.Services.AddCinemasData(builder.Configuration);
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
