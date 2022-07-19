using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPR.Shared.Domain.Models;

namespace MPR.Movies.Data.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasIndex(x => x.TheMovieDbId)
                .IsUnique();

            builder.Property(x => x.Name)
                .HasColumnType("varchar(255)");
        }
    }
}
