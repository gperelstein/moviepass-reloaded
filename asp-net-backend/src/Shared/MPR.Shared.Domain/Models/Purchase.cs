using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class Purchase : BaseEntity, IAuditable
    {
        public int NumberOfTickets { get; set; }
        public decimal Amount { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public IList<Ticket> Tickets { get; set; }
    }
}
