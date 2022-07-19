using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MPR.Movies.Configuration;
using MPR.Movies.Logic.Abstractions;
using MPR.Movies.Logic.Features.Movies.Extensions;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Pagination;
using MPR.Shared.Logic.Responses.Features.Movies;

namespace MPR.Movies.Logic.Features.Movies.Queries
{
    public class ListMovies
    {
        public class Query : PaginatedRequest<MovieResponse> { }

        public class Handler : PaginatedCommandHandler<Movie, Query, MovieResponse>
        {
            private readonly IMprMoviesDbContext _context;

            public Handler(IMprMoviesDbContext context, IOptions<MoviesServiceOptions> appOptions)
                : base(appOptions)
            {
                _context = context;
            }

            protected override MovieResponse Convert(Movie entity)
            {
                return entity.ToResponse();
            }

            protected override IQueryable<Movie> Query(Query request)
            {
                var movies = _context.Movies.AsNoTracking()
                                            .Include(x => x.Genres);

                return movies;
            }
        }
    }
}
