using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Pagination;
using MPR.Shared.Logic.Responses.Features.Shows;
using MPR.Shows.Configuration;
using MPR.Shows.Logic.Abstractions;
using MPR.Shows.Logic.Features.Shows.Extensions;

namespace MPR.Shows.Logic.Features.Shows.Queries
{
    public class ListShows
    {
        public class Query : PaginatedRequest<ShowResponse>
        {

        }

        public class Handler : PaginatedCommandHandler<Show, Query, ShowResponse>
        {
            private readonly IMprShowsDbContext _context;

            public Handler(IMprShowsDbContext context, IOptions<ShowsServiceOptions> appOptions)
                : base(appOptions)
            {
                _context = context;
            }

            protected override ShowResponse Convert(Show entity)
            {
                return entity.ToResponse();
            }

            protected override IQueryable<Show> Query(Query request)
            {
                var shows = _context.Shows.AsNoTracking();

                return shows;
            }
        }
    }
}
