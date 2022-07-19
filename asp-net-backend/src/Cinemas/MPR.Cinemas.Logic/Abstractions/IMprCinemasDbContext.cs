using Microsoft.EntityFrameworkCore;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;

namespace MPR.Cinemas.Logic.Abstractions
{
    public interface IMprCinemasDbContext : IMprDbContext
    {
        DbSet<Cinema> Cinemas { get; }
        DbSet<Room> Rooms { get; }
    }
}
