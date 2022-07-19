using Microsoft.EntityFrameworkCore;
using MPR.Shared.Data;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;
using MPR.Shows.Logic.Abstractions;

namespace MPR.Shows.Data
{
    public class MprShowsDbContext : MprDbContext, IMprShowsDbContext
    {
        public MprShowsDbContext(DbContextOptions<MprShowsDbContext> options, ICurrentUserService currentUserService) : base(options, currentUserService)
        {
        }

        public DbSet<Show> Shows { get; set; }
    }
}
