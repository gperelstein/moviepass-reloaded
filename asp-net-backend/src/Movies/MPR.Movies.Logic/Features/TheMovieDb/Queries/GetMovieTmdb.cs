using MediatR;
using MPR.Movies.Logic.Abstractions;
using MPR.Movies.Logic.Features.TheMovieDb.Responses;
using MPR.Shared.Logic.Responses;

namespace MPR.Movies.Logic.Features.TheMovieDb.Queries
{
    public class GetMovieTmdb
    {
        public class Query : IRequest<Response<MovieDetailsTmdbResponse>>
        {
            public int TheMovieDbId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<MovieDetailsTmdbResponse>>
        {
            private readonly ITheMovieDbClient _theMovieDbClient;

            public Handler(ITheMovieDbClient theMovieDbClient)
            {
                _theMovieDbClient = theMovieDbClient;
            }

            public async Task<Response<MovieDetailsTmdbResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var movieDetailTmdb = await _theMovieDbClient.GetMovieDetailsAsync(request.TheMovieDbId, cancellationToken);

                return new Response<MovieDetailsTmdbResponse> { Payload = movieDetailTmdb };
            }
        }
    }
}
