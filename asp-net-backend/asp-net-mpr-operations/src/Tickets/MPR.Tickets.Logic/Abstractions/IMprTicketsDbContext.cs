using Microsoft.EntityFrameworkCore;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;

namespace MPR.Tickets.Logic.Abstractions
{
    public interface IMprTicketsDbContext : IMprDbContext
    {
        DbSet<Purchase> Purchases { get; }
        DbSet<Ticket> Tickets { get; }
    }
}
