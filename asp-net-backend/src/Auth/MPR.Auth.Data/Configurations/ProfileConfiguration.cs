using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPR.Shared.Domain.Models;

namespace MPR.Auth.Data.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");

        builder.Property(x => x.FirstName)
            .HasColumnType("varchar(50)");

        builder.Property(x => x.LastName)
            .HasColumnType("varchar(50)");

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<Profile>(x => x.UserId);
    }
}
