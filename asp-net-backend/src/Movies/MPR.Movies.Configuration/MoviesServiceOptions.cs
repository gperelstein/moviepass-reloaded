using MPR.Movies.Configuration.Configuration;
using MPR.Shared.Configuration;

namespace MPR.Movies.Configuration
{
    public class MoviesServiceOptions : AppOptions
    {
        public TheMovieDbOptions TheMovieDb { get; set; }
    }
}
