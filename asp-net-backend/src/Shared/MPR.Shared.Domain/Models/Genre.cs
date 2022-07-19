using MPR.Shared.Domain.Abstractions;

namespace MPR.Shared.Domain.Models
{
    public class Genre : BaseEntity, INamedEntity
    {
        public int? TheMovieDbId { get; set; }
        public string Name { get; set; }
        public IList<Movie> Movies { get; set; }
    }
}
