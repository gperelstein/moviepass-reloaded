using MPR.Movies.Logic.Features.TheMovieDb.Responses;
using MPR.Movies.TheMovieDb.Models;
using MPR.Movies.TheMovieDb.Resources;

namespace MPR.Movies.TheMovieDb.Extensions
{
    public static class MoviesTmdbExtensions
    {
        public static MovieTmdbResponse ToMovieResponse(this MovieTmdbApiResponse movieResponseTmdb) => new MovieTmdbResponse
        {
            TheMovieDbId = movieResponseTmdb.Id,
            Title = movieResponseTmdb.Title,
            Poster = $"{ResourcesPath.PosterBasePath}{movieResponseTmdb.Image}",
            Language = movieResponseTmdb.Language,
            Overview = movieResponseTmdb.Overview
        };

        public static MovieDetailsTmdbResponse ToMovieDetailResponse(this MovieDetailsTmdbApiResponse movieResponseTmdb) => new MovieDetailsTmdbResponse
        {
            TheMovieDbId = movieResponseTmdb.Id,
            Title = movieResponseTmdb.Title,
            Poster = $"{ResourcesPath.PosterBasePath}{movieResponseTmdb.Image}",
            Language = movieResponseTmdb.Language,
            Overview = movieResponseTmdb.Overview,
            TagLine = movieResponseTmdb.TagLine,
            Trailer = $"{ResourcesPath.TrailerBasePath}{movieResponseTmdb.Trailer}",
            Duration = movieResponseTmdb.Duration,
            Genres = movieResponseTmdb.Genres.Select(x => x.ToGenreResponse()).ToList()
        };
    }
}
