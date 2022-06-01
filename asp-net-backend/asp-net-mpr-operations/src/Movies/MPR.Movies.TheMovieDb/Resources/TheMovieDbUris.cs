namespace MPR.Movies.TheMovieDb.Resources
{
    public static class TheMovieDbUris
    {
        public static string GetMovie(int id, string apiKey) => $"https://api.themoviedb.org/3/movie/{id}?api_key={apiKey}&language=en-US";

        public static string GetMovieVideos(int id, string apiKey) => $"https://api.themoviedb.org/3/movie/{id}/videos?api_key={apiKey}&language=en-US";

        public static string GetMoviesNowPlaying(int pageNumber, string apiKey) => $"https://api.themoviedb.org/3/movie/now_playing?api_key={apiKey}&language=en-US&page={pageNumber}";

        public static string ListGenres(string apiKey) => $"https://api.themoviedb.org/3/genre/movie/list?api_key={apiKey}&language=en-US";
    }
}
