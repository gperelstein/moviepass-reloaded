using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MPR.Cinemas.Configuration;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Cinemas.Logic.Features.Cinemas.Extensions;
using MPR.Cinemas.Logic.Features.Cinemas.Responses;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Pagination;

namespace MPR.Cinemas.Logic.Features.Cinemas.Queries
{
    public class ListCinemas
    {
        public class Query : PaginatedRequest<CinemaResponse> { }

        public class Handler : PaginatedCommandHandler<Cinema, Query, CinemaResponse>
        {
            private readonly IMprCinemasDbContext _context;

            public Handler(IMprCinemasDbContext context, IOptions<CinemasServiceOptions> appOptions)
                : base(appOptions)
            {
                _context = context;
            }

            protected override CinemaResponse Convert(Cinema entity)
            {
                return entity.ToResponse();
            }

            protected override IQueryable<Cinema> Query(Query request)
            {
                return _context.Cinemas.AsNoTracking();
            }
        }
    }
}
