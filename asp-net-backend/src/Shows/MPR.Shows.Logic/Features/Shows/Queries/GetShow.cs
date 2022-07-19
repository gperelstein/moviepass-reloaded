using MediatR;
using Microsoft.EntityFrameworkCore;
using MPR.Shared.Logic.Responses;
using MPR.Shared.Logic.Responses.Features.Shows;
using MPR.Shows.Logic.Abstractions;
using MPR.Shows.Logic.Errors;
using MPR.Shows.Logic.Features.Shows.Extensions;

namespace MPR.Shows.Logic.Features.Shows.Queries
{
    public class GetShow
    {
        public class Query : IRequest<Response<ShowResponse>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<ShowResponse>>
        {
            private readonly IMprShowsDbContext _context;

            public Handler(IMprShowsDbContext context)
            {
                _context = context;
            }

            public async Task<Response<ShowResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var show = await _context.Shows
                                         .AsNoTracking()
                                         .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (show == null)
                {
                    return Response.CreateBadRequestResponse<ShowResponse>(ErrorCodes.SHOW_NOTEXISTS,
                        $"Show with Id {request.Id} not exists");
                }

                return new Response<ShowResponse> { Payload = show.ToResponse() };
            }
        }
    }
}
