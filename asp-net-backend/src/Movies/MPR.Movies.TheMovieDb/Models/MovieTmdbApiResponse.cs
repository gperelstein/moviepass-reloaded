using System.Text.Json.Serialization;

namespace MPR.Movies.TheMovieDb.Models
{
    public class MovieTmdbApiResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("original_language")]
        public string Language { get; set; }

        [JsonPropertyName("poster_path")]
        public string Image { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }
    }
}
