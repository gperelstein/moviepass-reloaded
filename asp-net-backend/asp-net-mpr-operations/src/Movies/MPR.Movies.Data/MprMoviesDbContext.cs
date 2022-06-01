using Microsoft.EntityFrameworkCore;
using MPR.Movies.Logic.Abstractions;
using MPR.Shared.Data;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;

namespace MPR.Movies.Data
{
    public class MprMoviesDbContext : MprDbContext, IMprMoviesDbContext
    {
        public MprMoviesDbContext(DbContextOptions<MprMoviesDbContext> options, ICurrentUserService currentUserService) : base(options, currentUserService)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
