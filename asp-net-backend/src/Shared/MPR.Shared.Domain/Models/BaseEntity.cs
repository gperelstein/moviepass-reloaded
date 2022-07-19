using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class BaseEntity : ISortable
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
