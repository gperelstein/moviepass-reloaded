using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Cinemas.Logic.Errors;
using MPR.Cinemas.Logic.Features.Cinemas.Extensions;
using MPR.Cinemas.Logic.Features.Cinemas.Responses;
using MPR.Shared.Logic.Responses;

namespace MPR.Cinemas.Logic.Features.Cinemas.Queries
{
    public class GetCinema
    {
        public class Query : IRequest<Response<CinemaDetailedResponse>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<CinemaDetailedResponse>>
        {
            private readonly IMprCinemasDbContext _context;

            public Handler(IMprCinemasDbContext context)
            {
                _context = context;
            }

            public async Task<Response<CinemaDetailedResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cinema = await _context.Cinemas
                                           .AsNoTracking()
                                           .Include(x => x.Rooms)
                                           .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (cinema == null)
                {
                    return Response.CreateBadRequestResponse<CinemaDetailedResponse>(ErrorCodes.CINEMA_NOTEXISTS,
                        $"Cinema with Id {request.Id} not exists");
                }

                return new Response<CinemaDetailedResponse> { Payload = cinema.ToDetailedResponse() };
            }
        }
    }
}
