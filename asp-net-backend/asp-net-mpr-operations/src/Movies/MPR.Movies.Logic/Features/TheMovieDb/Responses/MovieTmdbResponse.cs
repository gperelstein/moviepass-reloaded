namespace MPR.Movies.Logic.Features.TheMovieDb.Responses
{
    public class MovieTmdbResponse
    {
        public int TheMovieDbId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Poster { get; set; }
        public string Overview { get; set; }
        public bool IsInDatabase { get; set; }
    }
}
