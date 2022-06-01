using System.Text.Json.Serialization;

namespace MPR.Movies.TheMovieDb.Models
{
    public class ListMovieTmdbApiResponse
    {
        [JsonPropertyName("page")]
        public int PageNumber { get; set; }

        [JsonPropertyName("results")]
        public List<MovieTmdbApiResponse> Movies { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
    }
}
