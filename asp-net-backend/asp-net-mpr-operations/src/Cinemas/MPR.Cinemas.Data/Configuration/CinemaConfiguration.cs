using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPR.Shared.Domain.Models;

namespace MPR.Cinemas.Data.Configuration
{
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.ToTable("Cinemas");

            builder.Property(x => x.Name)
                .HasColumnType("varchar(255)");

            builder.Property(x => x.Address)
                .HasColumnType("varchar(255)");

            builder.HasMany(x => x.Rooms)
                   .WithOne(x => x.Cinema)
                   .HasForeignKey(x => x.CinemaId);
        }
    }
}
