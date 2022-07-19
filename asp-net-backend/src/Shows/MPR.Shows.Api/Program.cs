using MPR.Shows.Api;
using MPR.Shows.Configuration;
using MPR.Shows.Data;
using MPR.Shows.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ShowsServiceOptions>(
    builder.Configuration.GetSection(ShowsServiceOptions.AppConfiguration));
builder.Services.AddShowsPresentation();
builder.Services.AddShowsLogic();
builder.Services.AddShowsData(builder.Configuration);
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
