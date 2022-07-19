using System.Text.Json.Serialization;

namespace MPR.Movies.TheMovieDb.Models
{
    public class MovieDetailsTmdbApiResponse : MovieTmdbApiResponse
    {
        [JsonPropertyName("runtime")]
        public int Duration { get; set; }

        [JsonPropertyName("tagline")]
        public string TagLine { get; set; }

        public string Trailer { get; set; }

        [JsonPropertyName("genres")]
        public IList<GenreTmdbApiResponse> Genres { get; set; }
    }
}
