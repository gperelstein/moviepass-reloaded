using System.Text.Json.Serialization;

namespace MPR.Movies.TheMovieDb.Models
{
    public class ListMovieVideoTmdbApiResponse
    {
        [JsonPropertyName("results")]
        public List<MovieVideoTmdbApiResponse> Videos { get; set; }
    }
}
