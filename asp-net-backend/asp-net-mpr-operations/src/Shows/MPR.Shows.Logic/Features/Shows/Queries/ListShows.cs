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
            public DateTime? From { get; set; }
            public DateTime? To { get; set; }
            public Guid? CinemaId { get; set; }
            public Guid? RoomId { get; set; }
            public Guid? MovieId { get; set; }
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
                var shows = _context.Shows
                    .AsNoTracking()
                    .Where(x => !request.From.HasValue || x.StartAt >= request.From.Value)
                    .Where(x => !request.To.HasValue || x.EndAt <= request.To.Value)
                    .Where(x => !request.CinemaId.HasValue || x.CinemaId == request.CinemaId.Value)
                    .Where(x => !request.RoomId.HasValue || x.RoomId == request.RoomId.Value)
                    .Where(x => !request.MovieId.HasValue || x.MovieId == request.MovieId.Value);

                return shows;
            }
        }
    }
}
