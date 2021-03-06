using MPR.Shared.Logic.Responses.Features.Genres;

namespace MPR.Shared.Logic.Responses.Features.Movies
{
    public class MovieResponse
    {
        public Guid Id { get; set; }
        public int? TheMovieDbId { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Language { get; set; }
        public string Poster { get; set; }
        public double Duration { get; set; }
        public string TagLine { get; set; }
        public string Trailer { get; set; }
        public IList<GenreResponse> Genres { get; set; }
        public Guid Owner { get; set; }
        public Guid LastUpdatedBy { get; set; }
    }
}
