using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class Payment : BaseEntity, IAuditable
    {
        public decimal Amount { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
