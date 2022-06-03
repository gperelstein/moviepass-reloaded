using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using MPR.Shared.Domain.Abstractions;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Abstractions;

namespace MPR.Shared.Data
{
    public abstract class MprDbContext : DbContext, IMprDbContext
    {
        private const string DefaultSchema = "mpr";
        private readonly ICurrentUserService _currentUserService;

        public MprDbContext(DbContextOptions options, ICurrentUserService currentUserService = null) : base(options)
        {
            _currentUserService = currentUserService;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(DefaultSchema);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            ConfigureIdProperty(modelBuilder);

            ConfigureDefaultDeletedMark(modelBuilder);

            ApplyISortableConfiguration(modelBuilder);
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
                        x.Property("DeletedBy").HasDefaultValue(null);
                        x.Property("DeletedAt").HasDefaultValue(null);
                    });
            }
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

        private void ApplyUpdatedDateAndAuditableInfo(Guid? userId)
        {
            var lastUpdatedAt = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<ISortable>()
                                                .Where(x => x.State == EntityState.Modified
                                                            || x.State == EntityState.Added))
            {
                entry.Entity.LastUpdatedAt = lastUpdatedAt;
            }


            foreach (var entry in ChangeTracker.Entries<IRemovable>().Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.Entity.MarkedAsDeleted = true;
                entry.Entity.DeletedBy = userId;
                entry.Entity.DeletedAt = lastUpdatedAt;
                foreach (var navigationEntry in entry.Navigations.Where(n => ((IReadOnlyNavigation)n.Metadata).IsOnDependent
                        && n.EntityEntry.GetType().IsAssignableFrom(typeof(IRemovable))))
                {
                    if (navigationEntry is CollectionEntry collectionEntry)
                    {
                        foreach (var dependentEntry in collectionEntry.CurrentValue)
                        {
                            HandleDependent(Entry(dependentEntry), userId, lastUpdatedAt);
                        }
                    }
                    else
                    {
                        var dependentEntry = navigationEntry.CurrentValue;
                        if (dependentEntry != null)
                        {
                            HandleDependent(Entry(dependentEntry), userId, lastUpdatedAt);
                        }
                    }
                }
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

        private void HandleDependent(EntityEntry entry, Guid? userId, DateTime lastUpdatedAt)
        {
            var entreParsed = (EntityEntry<IRemovable>)entry;
            entreParsed.State = EntityState.Modified;
            entreParsed.Entity.MarkedAsDeleted = true;
            entreParsed.Entity.DeletedBy = userId;
            entreParsed.Entity.DeletedAt = lastUpdatedAt;
        }
    }
}
