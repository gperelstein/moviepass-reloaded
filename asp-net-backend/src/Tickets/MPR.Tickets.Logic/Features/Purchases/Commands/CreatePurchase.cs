using MediatR;
using MPR.Shared.Domain.Models;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Purchases;
using MPR.Tickets.Logic.Abstractions;

namespace MPR.Tickets.Logic.Features.Purchases.Commands
{
    public class CreatePurchase
    {
        public class Command : IRequest<Response<PurchaseResponse>>
        {
            public int NumberOfTickets { get; set; }
            public Guid ShowId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response<PurchaseResponse>>
        {
            private readonly IMprTicketsDbContext _context;

            public Handler(IMprTicketsDbContext context)
            {
                _context = context;
            }

            public async Task<Response<PurchaseResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var newPurchase = new Purchase
                {
                    NumberOfTickets = request.NumberOfTickets,
                };

                var newTickets = Enumerable.Range(0, request.NumberOfTickets)
                    .Select(x => new Ticket { Qr = CreateQr() })
                    .ToList();

                await _context.Purchases.AddAsync(newPurchase, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response<PurchaseResponse> { Payload = new PurchaseResponse() };
            }

            private string CreateQr()
            {
                return "";
            }
        }
    }
}
