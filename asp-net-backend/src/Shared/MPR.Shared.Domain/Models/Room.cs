using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class Room : BaseEntity, IRemovable, IAuditable
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal TicketValue { get; set; }
        public Guid CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public bool MarkedAsDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
