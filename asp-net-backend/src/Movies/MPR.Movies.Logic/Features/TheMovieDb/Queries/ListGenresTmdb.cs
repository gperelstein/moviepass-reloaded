using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Movies.Logic.Abstractions;
using MPR.Movies.Logic.Features.TheMovieDb.Responses;
using MPR.Shared.Logic.Responses;

namespace MPR.Movies.Logic.Features.TheMovieDb.Queries
{
    public class ListGenresTmdb
    {
        public class Query : IRequest<Response<List<GenreTmdbResponse>>> { }

        public class Handler : IRequestHandler<Query, Response<List<GenreTmdbResponse>>>
        {
            private readonly ITheMovieDbClient _theMovieDbClient;
            private readonly IMprMoviesDbContext _context;

            public Handler(ITheMovieDbClient theMovieDbClient, IMprMoviesDbContext context)
            {
                _theMovieDbClient = theMovieDbClient;
                _context = context;
            }

            public async Task<Response<List<GenreTmdbResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var genresTmdbList = await _theMovieDbClient.ListGenresAsync(cancellationToken);

                var genreTheMovieDbIdsInDb = await _context.Genres.Where(x => x.TheMovieDbId.HasValue)
                                                                  .Select(x => x.TheMovieDbId)
                                                                  .ToListAsync(cancellationToken);

                foreach (var genreTmdb in genresTmdbList)
                {
                    genreTmdb.IsInDatabase = IsGenreInDb(genreTheMovieDbIdsInDb, genreTmdb);
                }

                return new Response<List<GenreTmdbResponse>> { Payload = genresTmdbList };
            }

            private bool IsGenreInDb(List<int?> genreTheMovieDbIdsInDb, GenreTmdbResponse genreTmdb)
            {
                if (genreTheMovieDbIdsInDb.Any(x => x == genreTmdb.TheMovieDbId))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
