using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Cinemas.Logic.Errors;
using MPR.Shared.Logic.Responses;

namespace MPR.Cinemas.Logic.Features.Cinemas.Commands
{
    public class DeleteCinema
    {
        public class Command : IRequest<Response<Unit>>
        {
            public Guid Id { get; set; }
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
                                           .Include(x => x.Rooms)
                                           .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (cinema == null)
                {
                    return Response.CreateBadRequestResponse<Unit>(ErrorCodes.CINEMA_NOTEXISTS,
                        $"Cinema with Id {request.Id} not exists");
                }

                _context.Cinemas.Remove(cinema);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response<Unit> { Payload = Unit.Value };
            }
        }
    }
}
