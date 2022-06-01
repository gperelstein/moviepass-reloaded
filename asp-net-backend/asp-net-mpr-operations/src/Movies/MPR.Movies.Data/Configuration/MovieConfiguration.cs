using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPR.Shared.Domain.Models;

namespace MPR.Movies.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");

            builder.HasIndex(x => x.TheMovieDbId)
                .IsUnique();

            builder.Property(x => x.Title)
                .HasColumnType("varchar(255)");

            builder.Property(x => x.Language)
                .HasColumnType("varchar(255)");

            builder.Property(x => x.Poster)
                .HasColumnType("varchar(255)");

            builder.Property(x => x.Duration)
                .HasColumnType("interval");

            builder.Property(x => x.TagLine)
                .HasColumnType("varchar(255)");

            builder.Property(x => x.Trailer)
                .HasColumnType("varchar(255)");

            builder.HasMany(x => x.Genres)
                   .WithMany(x => x.Movies)
                   .UsingEntity(x => x.ToTable("MovieGenres"));
        }
    }
}
