using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MPR.Shared.Configuration;
using MPR.Shared.Configuration.Configuration;
using MPR.Shared.Domain.Abstractions;
using MPR.Shared.Logic.Extensions;
using MPR.Shared.Logic.Responses;
using System.Linq.Dynamic.Core;

namespace MPR.Shared.Logic.Pagination
{
    public abstract class PaginatedCommandHandler<TEntity, TCommand, TResponse> : IRequestHandler<TCommand, Response<PaginatedResult<TResponse>>>
        where TCommand : PaginatedRequest<TResponse>
        where TEntity : ISortable
    {
        private readonly PaginationOptions _paginationOptions;

        protected PaginatedCommandHandler(IOptions<AppOptions> appOptions)
        {
            _paginationOptions = appOptions.Value.Pagination;
        }

        public async Task<Response<PaginatedResult<TResponse>>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int pageNumber = PaginationHelper.GetPageNumber(request);
                int resultsPerPage = PaginationHelper.GetRowsPerPage(request, _paginationOptions.MaxPageSize);
                int skipRows = PaginationHelper.GetSkipRows(resultsPerPage, pageNumber);

                var query = Query(request).OrderBy($"{request.SortingName} {request.SortType}");
                var totalResults = await query.CountAsync(cancellationToken);
                var result = await query
                                    .Skip(skipRows)
                                    .Take(resultsPerPage)
                                    .ToListAsync(cancellationToken);

                return new Response<PaginatedResult<TResponse>>
                {
                    Payload = new PaginatedResult<TResponse>
                    {
                        PageNumber = pageNumber,
                        PageSize = resultsPerPage,
                        TotalResults = totalResults,
                        Items = result.Select(x => Convert(x))
                    }
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"An error has occurred while generating the list of {nameof(TEntity)} elements.");
                return ex.ToResponse<PaginatedResult<TResponse>>();
            }
        }

        protected abstract IQueryable<TEntity> Query(TCommand request);

        protected abstract TResponse Convert(TEntity entity);
    }
}
