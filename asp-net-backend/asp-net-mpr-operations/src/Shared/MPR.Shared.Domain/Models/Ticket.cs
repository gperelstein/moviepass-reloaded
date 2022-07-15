using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class Ticket : BaseEntity, IAuditable, IRemovable
    {
        public string Qr { get; set; }
        public Guid ShowId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public bool MarkedAsDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
