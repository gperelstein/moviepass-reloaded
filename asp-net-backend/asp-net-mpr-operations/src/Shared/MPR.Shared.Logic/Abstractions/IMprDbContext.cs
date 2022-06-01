namespace MPR.Shared.Logic.Abstractions
{
    public interface IMprDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(Guid userId, CancellationToken cancellationToken);
    }
}
