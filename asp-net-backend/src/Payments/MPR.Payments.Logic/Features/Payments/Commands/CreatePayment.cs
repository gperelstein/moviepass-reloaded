using MediatR;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Payments;

namespace MPR.Payments.Logic.Features.Payments.Commands
{
    public class CreatePayment
    {
        public class Command : IRequest<Response<PaymentResponse>>
        {
            public decimal Amount { get; set; }
            public Guid PurchaseId { get; set; }
        }
    }
}
