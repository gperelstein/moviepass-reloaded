using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Cinemas.Logic.Errors;
using MPR.Shared.Logic.Responses;
using System.Text.Json.Serialization;

namespace MPR.Cinemas.Logic.Features.Cinemas.Commands
{
    public class DeleteRoom
    {
        public class Command : IRequest<Response<Unit>>
        {
            public Guid CinemaId { get; set; }
            public Guid RoomId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response<Unit>>
        {
            private readonly IMprCinemasDbContext _context;

            public Handler(IMprCinemasDbContext context)
            {
                _context = context;
            }

            public async Task<Response<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var cinema = await _context.Cinemas
                                           .Include(x => x.Rooms.Where(x => x.Id == request.RoomId))
                                           .SingleOrDefaultAsync(x => x.Id == request.CinemaId, cancellationToken);

                if (cinema == null)
                {
                    return Response.CreateBadRequestResponse<Unit>(ErrorCodes.CINEMA_NOTEXISTS,
                        $"Cinema with Id {request.CinemaId} not exists");
                }

                var room = cinema.Rooms.SingleOrDefault();

                if (room == null)
                {
                    return Response.CreateBadRequestResponse<Unit>(ErrorCodes.ROOM_NOTEXISTS,
                        $"Room with Id {request.RoomId} not exists");
                }

                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response<Unit> { Payload = Unit.Value };
            }
        }
    }
}
