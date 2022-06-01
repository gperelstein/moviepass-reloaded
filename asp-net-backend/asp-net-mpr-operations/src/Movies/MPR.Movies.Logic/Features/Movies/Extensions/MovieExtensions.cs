using MPR.Movies.Logic.Features.Genres.Extensions;
using MPR.Movies.Logic.Features.Movies.Responses;
using MPR.Shared.Domain.Models;

namespace MPR.Movies.Logic.Features.Movies.Extensions
{
    public static class MovieExtensions
    {
        public static MovieResponse ToResponse(this Movie movie)
        {
            var movieResponse = new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                TheMovieDbId = movie.TheMovieDbId,
                Trailer = movie.Trailer,
                Duration = movie.Duration.TotalMinutes,
                Genres = movie.Genres.Select(x => x.ToResponse()).ToList(),
                Language = movie.Language,
                LastUpdatedBy = movie.LastUpdatedBy,
                Owner = movie.Owner,
                Poster = movie.Poster,
                TagLine = movie.TagLine,
            };

            return movieResponse;
        }
    }
}
