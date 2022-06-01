using System.Text.Json.Serialization;

namespace MPR.Movies.TheMovieDb.Models
{
    public class GenreTmdbApiResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
