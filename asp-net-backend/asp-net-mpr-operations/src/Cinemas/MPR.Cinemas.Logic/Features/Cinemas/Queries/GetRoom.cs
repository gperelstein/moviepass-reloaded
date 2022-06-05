using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Cinemas.Logic.Errors;
using MPR.Cinemas.Logic.Features.Cinemas.Extensions;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Cinemas;

namespace MPR.Cinemas.Logic.Features.Cinemas.Queries
{
    public class GetRoom
    {
        public class Query : IRequest<Response<RoomResponse>>
        {
            public Guid CinemaId { get; set; }
            public Guid RoomId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<RoomResponse>>
        {
            private readonly IMprCinemasDbContext _context;

            public Handler(IMprCinemasDbContext context)
            {
                _context = context;
            }

            public async Task<Response<RoomResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cinema = await _context.Cinemas
                                           .AsNoTracking()
                                           .Include(x => x.Rooms.Where(y => y.Id == request.RoomId))
                                           .SingleOrDefaultAsync(x => x.Id == request.CinemaId, cancellationToken);

                if (cinema == null)
                {
                    return Response.CreateBadRequestResponse<RoomResponse>(ErrorCodes.CINEMA_NOTEXISTS,
                        $"Cinema with Id {request.CinemaId} not exists");
                }

                var room = cinema.Rooms.SingleOrDefault();

                if (room == null)
                {
                    return Response.CreateBadRequestResponse<RoomResponse>(ErrorCodes.ROOM_NOTEXISTS,
                        $"Room with Id {request.RoomId} not exists");
                }

                return new Response<RoomResponse> { Payload = room.ToResponse() };
            }
        }
    }
}
