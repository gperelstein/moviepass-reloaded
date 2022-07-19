using MPR.Movies.Logic.Features.TheMovieDb.Responses;
using MPR.Movies.TheMovieDb.Models;

namespace MPR.Movies.TheMovieDb.Extensions
{
    public static class GenresTmdbExtensions
    {
        public static GenreTmdbResponse ToGenreResponse(this GenreTmdbApiResponse genreResponseTmdb) => new GenreTmdbResponse
        {
            TheMovieDbId = genreResponseTmdb.Id,
            Name = genreResponseTmdb.Name
        };
    }
}
