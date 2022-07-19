using System.Text.Json.Serialization;

namespace MPR.Movies.TheMovieDb.Models
{
    public class MovieVideoTmdbApiResponse
    {
        [JsonPropertyName("key")]
        public string VideoKey { get; set; }

        [JsonPropertyName("site")]
        public string Site { get; set; }
    }
}
