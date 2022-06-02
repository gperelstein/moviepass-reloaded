using Microsoft.EntityFrameworkCore;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Shared.Data;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;

namespace MPR.Cinemas.Data
{
    public class MprCinemasDbContext : MprDbContext, IMprCinemasDbContext
    {
        public MprCinemasDbContext(DbContextOptions<MprCinemasDbContext> options, ICurrentUserService currentUserService) : base(options, currentUserService)
        {
        }

        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
