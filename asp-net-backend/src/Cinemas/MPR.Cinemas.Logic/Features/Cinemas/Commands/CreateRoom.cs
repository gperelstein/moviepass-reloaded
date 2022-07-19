using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Cinemas.Logic.Errors;
using MPR.Cinemas.Logic.Features.Cinemas.Extensions;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Cinemas;
using NJsonSchema.Annotations;
using System.Text.Json.Serialization;

namespace MPR.Cinemas.Logic.Features.Cinemas.Commands
{
    public class CreateRoom
    {
        [JsonSchema("CreateRoomCommand")]
        public class Command : IRequest<Response<RoomResponse>>
        {
            [JsonIgnore]
            public Guid CinemaId { get; set; }
            public string Name { get; set; }
            public int Capacity { get; set; }
            public decimal TicketValue { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Capacity).GreaterThan(0);
                RuleFor(x => x.TicketValue).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<Command, Response<RoomResponse>>
        {
            private readonly IMprCinemasDbContext _context;

            public Handler(IMprCinemasDbContext context)
            {
                _context = context;
            }

            public async Task<Response<RoomResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var cinema = await _context.Cinemas.SingleOrDefaultAsync(x => x.Id == request.CinemaId, cancellationToken);

                if (cinema == null)
                {
                    return Response.CreateBadRequestResponse<RoomResponse>(ErrorCodes.CINEMA_NOTEXISTS,
                        $"Cinema with Id {request.CinemaId} not exists");
                }

                var newRoom = new Room
                {
                    Name = request.Name,
                    Capacity = request.Capacity,
                    TicketValue = request.TicketValue,
                    CinemaId = request.CinemaId,
                };

                await _context.Rooms.AddAsync(newRoom, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response<RoomResponse> { Payload = newRoom.ToResponse() };
            }
        }
    }
}
