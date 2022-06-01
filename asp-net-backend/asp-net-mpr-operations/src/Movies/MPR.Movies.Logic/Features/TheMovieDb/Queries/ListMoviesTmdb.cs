using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Movies.Logic.Abstractions;
using MPR.Movies.Logic.Features.TheMovieDb.Responses;
using MPR.Shared.Logic.Pagination;
using MPR.Shared.Logic.Responses;

namespace MPR.Movies.Logic.Features.TheMovieDb.Queries
{
    public class ListMoviesTmdb
    {
        public class Query : IRequest<Response<PaginatedResult<MovieTmdbResponse>>>
        {
            public int PageNumber { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<PaginatedResult<MovieTmdbResponse>>>
        {
            private readonly ITheMovieDbClient _theMovieDbClient;
            private readonly IMprMoviesDbContext _context;

            public Handler(ITheMovieDbClient theMovieDbClient, IMprMoviesDbContext context)
            {
                _theMovieDbClient = theMovieDbClient;
                _context = context;
            }

            public async Task<Response<PaginatedResult<MovieTmdbResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var moviesTmdbPaginated = await _theMovieDbClient.ListMoviesAsync(request.PageNumber, cancellationToken);
                var moviesTmdbIds = moviesTmdbPaginated.Items.Select(x => x.TheMovieDbId).ToList();

                var movieTheMovieDbIdsInDb = await _context.Movies.Where(x => x.TheMovieDbId.HasValue)
                                                                  .Where(x => moviesTmdbIds.Contains(x.TheMovieDbId.Value))
                                                                  .Select(x => x.TheMovieDbId)
                                                                  .ToListAsync(cancellationToken);
                var movies = moviesTmdbPaginated.Items.ToList();
                foreach (var movieTmdb in movies)
                {
                    movieTmdb.IsInDatabase = IsMovieInDb(movieTheMovieDbIdsInDb, movieTmdb);
                }

                moviesTmdbPaginated.Items = movies;
                return new Response<PaginatedResult<MovieTmdbResponse>> { Payload = moviesTmdbPaginated };
            }

            private bool IsMovieInDb(List<int?> movieTheMovieDbIdsInDb, MovieTmdbResponse movieTmdb)
            {
                if (movieTheMovieDbIdsInDb.Any(x => x == movieTmdb.TheMovieDbId))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
