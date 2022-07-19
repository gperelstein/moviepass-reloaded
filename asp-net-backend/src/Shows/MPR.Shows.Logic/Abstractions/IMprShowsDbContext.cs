using Microsoft.EntityFrameworkCore;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;

namespace MPR.Shows.Logic.Abstractions
{
    public interface IMprShowsDbContext : IMprDbContext
    {
        DbSet<Show> Shows { get; }
    }
}
