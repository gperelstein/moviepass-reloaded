using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MPR.Auth.Data.IdentityCustomDbContext;
using MPR.Shared.Domain.Abstractions;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;

namespace MPR.Auth.Data;

public class MprAuthDbContext : KeyApiAuthorizationDbContext<User, Role, Guid>
{
    private const string DefaultSchema = "mpr";
    private readonly ICurrentUserService _currentUserService;

    public MprAuthDbContext(DbContextOptions<MprAuthDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        ICurrentUserService currentUserService = null) : base(options, operationalStoreOptions)
    {
        _currentUserService = currentUserService;
    }

    private static void ConfigureIdProperty(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            .Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity))))
        {
            modelBuilder.Entity(
                entityType.Name,
                x =>
                {
                    x.Property("Id").HasDefaultValueSql("uuid_generate_v4()");
                });
        }
    }

    private static void ConfigureDefaultDeletedMark(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            .Where(t => t.ClrType.GetInterfaces().Contains(typeof(IRemovable))))
        {
            modelBuilder.Entity(
                entityType.Name,
                x =>
                {
                    x.Property("MarkedAsDeleted").HasDefaultValue(false);
                });
        }
    }

    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        ConfigureIdProperty(modelBuilder);

        ConfigureDefaultDeletedMark(modelBuilder);

        ApplyISortableConfiguration(modelBuilder);
    }

    private void ApplyISortableConfiguration(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISortable).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>(nameof(ISortable.CreatedAt))
                    .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>(nameof(ISortable.LastUpdatedAt))
                    .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var userId = _currentUserService.GetUserId();
        ApplyUpdatedDateAndAuditableInfo(userId);

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(Guid userId, CancellationToken cancellationToken = new CancellationToken())
    {
        ApplyUpdatedDateAndAuditableInfo(userId);
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyUpdatedDateAndAuditableInfo(Guid? userId)
    {
        var lastUpdatedAt = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<ISortable>()
                                            .Where(x => x.State == EntityState.Modified
                                                        || x.State == EntityState.Added))
        {
            entry.Entity.LastUpdatedAt = lastUpdatedAt;
        }

        if (!userId.HasValue)
        {
            return;
        }

        foreach (var entry in ChangeTracker.Entries<IAuditable>()
                                            .Where(x => x.State == EntityState.Modified
                                                        || x.State == EntityState.Added))
        {
            entry.Entity.LastUpdatedAt = lastUpdatedAt;
            entry.Entity.LastUpdatedBy = userId.Value;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.Owner = userId.Value;
            }
        }
    }
}
