using Microsoft.EntityFrameworkCore;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;

namespace MPR.Movies.Logic.Abstractions
{
    public interface IMprMoviesDbContext : IMprDbContext
    {
        DbSet<Movie> Movies { get; }
        DbSet<Genre> Genres { get; }
    }
}
