using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class Show : BaseEntity, IRemovable, IAuditable
    {
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public Guid MovieId { get; set; }
        public Guid CinemaId { get; set; }
        public Guid RoomId { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public bool MarkedAsDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
