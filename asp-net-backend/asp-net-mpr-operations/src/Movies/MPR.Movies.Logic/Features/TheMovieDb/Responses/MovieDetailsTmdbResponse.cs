namespace MPR.Movies.Logic.Features.TheMovieDb.Responses
{
    public class MovieDetailsTmdbResponse : MovieTmdbResponse
    {
        public string TagLine { get; set; }
        public string Trailer { get; set; }
        public int Duration { get; set; }
        public IList<GenreTmdbResponse> Genres { get; set; }
    }
}
