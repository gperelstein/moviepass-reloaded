using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class Cinema : BaseEntity, IRemovable, IAuditable
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IList<Room> Rooms { get; set; }
        public bool MarkedAsDeleted { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
