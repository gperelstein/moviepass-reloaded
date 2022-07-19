using MPR.Movies.Logic.Features.TheMovieDb.Responses;
using MPR.Shared.Logic.Pagination;

namespace MPR.Movies.Logic.Abstractions
{
    public interface ITheMovieDbClient
    {
        Task<List<GenreTmdbResponse>> ListGenresAsync(CancellationToken cancellationToken);
        Task<PaginatedResult<MovieTmdbResponse>> ListMoviesAsync(int pageNumber, CancellationToken cancellationToken);
        Task<MovieDetailsTmdbResponse> GetMovieDetailsAsync(int movieId, CancellationToken cancellationToken);
    }
}
