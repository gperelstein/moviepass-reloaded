using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class Movie : BaseEntity, IRemovable, IAuditable
    {
        public int? TheMovieDbId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Poster { get; set; }
        public TimeSpan Duration { get; set; }
        public string TagLine { get; set; }
        public string Trailer { get; set; }
        public IList<Genre> Genres { get; set; }
        public bool MarkedAsDeleted { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
