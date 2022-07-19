using System.Text.Json.Serialization;

namespace MPR.Movies.TheMovieDb.Models
{
    public class ListGenresTmdbApiResponse
    {
        [JsonPropertyName("genres")]
        public List<GenreTmdbApiResponse> Genres { get; set; }
    }
}
