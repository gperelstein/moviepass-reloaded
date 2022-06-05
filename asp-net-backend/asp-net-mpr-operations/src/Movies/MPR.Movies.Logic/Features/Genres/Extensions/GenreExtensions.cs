using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses.Features.Genres;

namespace MPR.Movies.Logic.Features.Genres.Extensions
{
    public static class GenreExtensions
    {
        public static GenreResponse ToResponse(this Genre genre)
        {
            var genreRespose = new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name,
                TheMovieDbId = genre.TheMovieDbId
            };

            return genreRespose;
        }
    }
}
