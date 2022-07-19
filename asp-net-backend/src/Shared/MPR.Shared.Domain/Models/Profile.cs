using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class Profile : BaseEntity, IAuditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
