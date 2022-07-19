using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPR.Shared.Domain.Models;

namespace MPR.Cinemas.Data.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.Property(x => x.Name)
                .HasColumnType("varchar(255)");

            builder.Property(x => x.Capacity)
                .HasColumnType("int");

            builder.Property(x => x.TicketValue)
                .HasColumnType("decimal");
        }
    }
}
