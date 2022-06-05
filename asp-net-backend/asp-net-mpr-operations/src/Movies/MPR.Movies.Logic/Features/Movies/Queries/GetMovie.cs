using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Movies.Logic.Abstractions;
using MPR.Movies.Logic.Errors;
using MPR.Movies.Logic.Features.Movies.Extensions;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Movies;

namespace MPR.Movies.Logic.Features.Movies.Queries
{
    public class GetMovie
    {
        public class Query : IRequest<Response<MovieResponse>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<MovieResponse>>
        {
            private readonly IMprMoviesDbContext _context;

            public Handler(IMprMoviesDbContext context)
            {
                _context = context;
            }

            public async Task<Response<MovieResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var movie = await _context.Movies
                                           .AsNoTracking()
                                           .Include(x => x.Genres)
                                           .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (movie == null)
                {
                    return Response.CreateBadRequestResponse<MovieResponse>(ErrorCodes.MOVIE_NOTEXISTS,
                        $"Movie with Id {request.Id} not exists");
                }

                return new Response<MovieResponse> { Payload = movie.ToResponse() };
            }
        }
    }
}
