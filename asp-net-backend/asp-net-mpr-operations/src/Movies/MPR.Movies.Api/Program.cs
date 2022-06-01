using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MPR.Movies.Api;
using MPR.Movies.Configuration;
using MPR.Movies.Configuration.Configuration;
using MPR.Movies.Data;
using MPR.Movies.Logic;
using MPR.Movies.TheMovieDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MoviesServiceOptions>(
    builder.Configuration.GetSection(MoviesServiceOptions.AppConfiguration));
builder.Services.AddHttpContextAccessor();
builder.Services.AddMoviePresentation();
builder.Services.AddMoviesLogic();
builder.Services.AddMoviesData(builder.Configuration);
builder.Services.AddTheMovieDb();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");
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
