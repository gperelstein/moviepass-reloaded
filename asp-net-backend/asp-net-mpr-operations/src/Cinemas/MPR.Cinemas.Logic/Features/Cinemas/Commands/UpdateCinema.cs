using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Cinemas.Logic.Errors;
using MPR.Cinemas.Logic.Features.Cinemas.Extensions;
using MPR.Cinemas.Logic.Features.Cinemas.Responses;
using MPR.Shared.Logic.Responses;
using NJsonSchema.Annotations;
using System.Text.Json.Serialization;

namespace MPR.Cinemas.Logic.Features.Cinemas.Commands
{
    public class UpdateCinema
    {
        [JsonSchema("UpdateCinemaCommand")]
        public class Command : IRequest<Response<CinemaResponse>>
        {
            [JsonIgnore]
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Address).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Response<CinemaResponse>>
        {
            private readonly IMprCinemasDbContext _context;

            public Handler(IMprCinemasDbContext context)
            {
                _context = context;
            }

            public async Task<Response<CinemaResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var cinema = await _context.Cinemas.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (cinema == null)
                {
                    return Response.CreateBadRequestResponse<CinemaResponse>(ErrorCodes.CINEMA_NOTEXISTS,
                        $"Cinema with Id {request.Id} not exists");
                }

                cinema.Name = request.Name;
                cinema.Address = request.Address;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response<CinemaResponse> { Payload = cinema.ToResponse() };
            }
        }
    }
}
