using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Cinemas.Logic.Errors;
using MPR.Cinemas.Logic.Features.Cinemas.Extensions;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Cinemas;
using MPR.Shared.Messaging.Abstractions;

namespace MPR.Cinemas.Logic.Features.Cinemas.Queries
{
    public class ListRooms
    {
        public class Query : IRequest<Response<List<RoomResponse>>>
        {
            public Guid CinemaId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<List<RoomResponse>>>
        {
            private readonly IMprCinemasDbContext _context;

            public Handler(IMprCinemasDbContext context)
            {
                _context = context;
            }

            public async Task<Response<List<RoomResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cinema = await _context.Cinemas
                                           .AsNoTracking()
                                           .Include(x => x.Rooms)
                                            .ThenInclude(x => x.Cinema)
                                           .SingleOrDefaultAsync(x => x.Id == request.CinemaId, cancellationToken);

                if (cinema == null)
                {
                    return Response.CreateBadRequestResponse<List<RoomResponse>>(ErrorCodes.CINEMA_NOTEXISTS,
                        $"Cinema with Id {request.CinemaId} not exists");
                }

                return new Response<List<RoomResponse>> { Payload = cinema.Rooms.Select(x => x.ToResponse()).ToList() };
            }
        }
    }
}
