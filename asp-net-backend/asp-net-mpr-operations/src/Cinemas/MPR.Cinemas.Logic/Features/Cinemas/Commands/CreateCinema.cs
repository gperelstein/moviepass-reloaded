using FluentValidation;
using MediatR;
using MPR.Cinemas.Logic.Abstractions;
using MPR.Cinemas.Logic.Features.Cinemas.Extensions;
using MPR.Cinemas.Logic.Features.Cinemas.Responses;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses;
using NJsonSchema.Annotations;

namespace MPR.Cinemas.Logic.Features.Cinemas.Commands
{
    public class CreateCinema
    {
        [JsonSchema("CreateCinemaCommand")]
        public class Command : IRequest<Response<CinemaResponse>>
        {
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
                var newCinema = new Cinema
                {
                    Name = request.Name,
                    Address = request.Address,
                };

                await _context.Cinemas.AddAsync(newCinema, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response<CinemaResponse> { Payload = newCinema.ToResponse() };
            }
        }
    }
}
